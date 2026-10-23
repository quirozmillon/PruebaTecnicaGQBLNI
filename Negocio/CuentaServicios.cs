using Datos.Abstracciones;
using Datos.Entidades;
using Datos.Enum;
using Microsoft.AspNetCore.Http;
using Negocio.Abstracciones;
using Negocio.Abstracciones.Servicios;
using Negocio.Dto.Cuentas;
using Negocio.Dto.Transacciones;
using Negocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class CuentaServicios(
        ICuentaRepositorio cuentaRepositorio,
        ITrabajoUnico trabajoUnico,
        IGeneradorNoCuenta generadorNoCuenta,
        ITransaccionRepositorio transaccionRepositorio
        ) : ICuentaServicios
    {
        public async Task<CuentaRespuesta> BuscarPorIdAsync(string numeroCuenta)
        {
            var cuenta = await cuentaRepositorio.ObtenerPorIdCuenta(numeroCuenta);
            return new CuentaRespuesta
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Saldo = cuenta.Saldo,
                FechaCreacion = cuenta.FechaCreacion,
                ClienteId = cuenta.ClienteId

            };
        }

        public async Task<CuentaRespuesta> CrearAsync(CuentaSolicitud solicitud)
        {
            var nuevoNumero = generadorNoCuenta.GenerarNoCuenta();
            var cuentaExistente = await cuentaRepositorio.ObtenerPorIdCuenta(nuevoNumero);
            if (cuentaExistente != null)
            {
                throw new ExcepcionNegocio("Ya existe una cuenta con el número generado. Intente nuevamente.", StatusCodes.Status400BadRequest);
            }

            var nuevaCuenta = new Cuenta
            {
                NumeroCuenta = nuevoNumero,
                SaldoInicial = solicitud.SaldoInicial,
                Saldo = solicitud.SaldoInicial,
                FechaCreacion = DateTime.UtcNow,
                ClienteId = solicitud.ClienteId,
                Transacciones = new List<Transaccion>() { new Transaccion
                {
                    Tipo = TipoTransaccion.Deposito,
                    Monto = solicitud.SaldoInicial,
                    FechaProceso = DateTime.UtcNow,
                    Descripcion = "Depósito inicial"
                } }
            };
            if(string.IsNullOrEmpty(nuevaCuenta.NumeroCuenta))
            {
                throw new ExcepcionNegocio("No se pudo generar un número de cuenta válido.", StatusCodes.Status500InternalServerError);
            }
            cuentaRepositorio.Agregar(nuevaCuenta);
            await trabajoUnico.GuardarCambiosAsync();
            return new CuentaRespuesta
            {
                NumeroCuenta = nuevaCuenta.NumeroCuenta,
                SaldoInicial = nuevaCuenta.SaldoInicial,
                Saldo = nuevaCuenta.Saldo,
                FechaCreacion = nuevaCuenta.FechaCreacion,
                ClienteId = nuevaCuenta.ClienteId
            };
        }

        public async Task<CuentaRespuesta> DepositoAsync(TransaccionSolicitud solicitud)
        {
            if (string.IsNullOrEmpty(solicitud.NumeroCuenta))
            {
                throw new ExcepcionNegocio("La cuenta no puede estar vacia.", StatusCodes.Status404NotFound);
            }
            
            var cuenta = await cuentaRepositorio.ObtenerPorIdCuenta(solicitud.NumeroCuenta);
            if (cuenta == null)
            {
                throw new ExcepcionNegocio("La cuenta especificada no existe o no fue encontrada.", StatusCodes.Status404NotFound);
            }

            var nuevaTransaccion = new Transaccion
            {
                NumeroCuenta = solicitud.NumeroCuenta,
                Tipo = TipoTransaccion.Deposito,
                Monto = solicitud.Monto,
                FechaProceso = DateTime.UtcNow,
                Descripcion = solicitud.Descripcion
            };

            transaccionRepositorio.Agregar(nuevaTransaccion);
            cuenta.Saldo += solicitud.Monto;
            cuentaRepositorio.Actualizar(cuenta);
            await trabajoUnico.GuardarCambiosAsync();

            return new CuentaRespuesta
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Saldo = cuenta.Saldo,
                FechaCreacion = cuenta.FechaCreacion,
                ClienteId = cuenta.ClienteId
            };

        }

        public async Task<IList<CuentaRespuesta>> ObtenerTodasCuentasAsync()
        {
            var resultado = await cuentaRepositorio.ObtenerCuentasAsync();
            return resultado
                    .Select(c => new CuentaRespuesta
                    {
                        NumeroCuenta = c.NumeroCuenta,
                        SaldoInicial = c.SaldoInicial,
                        Saldo = c.Saldo,
                        FechaCreacion = c.FechaCreacion,
                        ClienteId = c.ClienteId
                    }).ToList();
        }

        public async Task<IList<TransaccionRespuesta>> ObtenerTransaccionesPorCuentaAsync(string numeroCuenta)
        {
            var resultado = await transaccionRepositorio.ObtenerPorNumeroCuentaAsync(numeroCuenta);
            return resultado
                    .Select(t => new TransaccionRespuesta
                    {
                        TransaccionId = t.TransaccionId,
                        Fecha = t.FechaProceso,
                        Descripcion = t.Descripcion,
                        TipoTransaccion = t.Tipo,
                        Monto = t.Monto
                    }).ToList();
        }

        public async Task<CuentaRespuesta> RetiroAsync(TransaccionSolicitud solicitud)
        {
            if (string.IsNullOrEmpty(solicitud.NumeroCuenta))
            {
                throw new ExcepcionNegocio("La cuenta no puede estar vacia.", StatusCodes.Status404NotFound);
            }

            var cuenta = await cuentaRepositorio.ObtenerPorIdCuenta(solicitud.NumeroCuenta);
            if (cuenta == null)
            {
                throw new ExcepcionNegocio("La cuenta especificada no existe o no fue encontrada.", StatusCodes.Status404NotFound);
            }


            if (cuenta.Saldo < solicitud.Monto)
            {
                throw new ExcepcionNegocio("El monto del depósito no puede ser mayor que el saldo actual de la cuenta.", StatusCodes.Status400BadRequest);
            }

            var nuevaTransaccion = new Transaccion
            {
                NumeroCuenta = solicitud.NumeroCuenta,
                Tipo = TipoTransaccion.Retiro,
                Monto = solicitud.Monto,
                FechaProceso = DateTime.UtcNow,
                Descripcion = solicitud.Descripcion
            };

            transaccionRepositorio.Agregar(nuevaTransaccion);
            cuenta.Saldo -= solicitud.Monto;
            cuentaRepositorio.Actualizar(cuenta);
            await trabajoUnico.GuardarCambiosAsync();

            return new CuentaRespuesta
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Saldo = cuenta.Saldo,
                FechaCreacion = cuenta.FechaCreacion,
                ClienteId = cuenta.ClienteId
            };

        }
    }
}
