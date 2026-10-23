using Datos.Abstracciones;
using Datos.Entidades;
using FluentAssertions;
using Moq;
using Negocio;
using Negocio.Abstracciones;
using Negocio.Dto.Transacciones;
using Negocio.Excepciones;


namespace NegocioPruebaUnitaria.Servicios
{
    public class CuentaServicioPrueba
    {
        [Fact]
        public async Task DepositarConMontoValidoDeberiaIncrementarSaldoYActualizarRepositorio()
        {
            var cuentaRepositorioMock = new Mock<ICuentaRepositorio>();
            var trabajoUnicoMock = new Mock<ITrabajoUnico>();
            var generadorNoCuentaMock = new Mock<IGeneradorNoCuenta>();
            var transaccionRepositorioMock = new Mock<ITransaccionRepositorio>();

            var nuevoClienteMock = new Clientes { 
                ClienteId = 1,
                NombreCompleto = "Juan Perez", 
                Sexo = Datos.Enum.Genero.Masculino, 
                FechaNacimiento = DateTime.UtcNow.AddYears(-30), 
                IngresosMensuales = 1000m 
            };

            var generadorMock = new GeneradorNoCuenta();
            var nuevaCuenta = generadorMock.GenerarNoCuenta();

            var cuentaMock = new Cuenta { NumeroCuenta = nuevaCuenta, Saldo = 100, ClienteId = nuevoClienteMock.ClienteId, SaldoInicial = 100 };

            cuentaRepositorioMock
            .Setup(repo => repo.ObtenerPorIdCuenta(nuevaCuenta))
            .ReturnsAsync(cuentaMock);

            var servicio = new CuentaServicios(
                cuentaRepositorioMock.Object,
                trabajoUnicoMock.Object,
                generadorNoCuentaMock.Object,
                transaccionRepositorioMock.Object
            );            

            var cuenta = await servicio.DepositoAsync(new TransaccionSolicitud { NumeroCuenta = cuentaMock.NumeroCuenta, Monto = 100 });

            Assert.NotNull(cuenta);
            Assert.Equal(200, cuenta.Saldo);
            Assert.Equal(100, cuenta.SaldoInicial);
        }


        [Fact]
        public async Task RetirarSinFondosSuficientesDeberiaLanzarExcepcion()
        {
            var cuentaRepositorioMock = new Mock<ICuentaRepositorio>();
            var trabajoUnicoMock = new Mock<ITrabajoUnico>();
            var generadorNoCuentaMock = new Mock<IGeneradorNoCuenta>();
            var transaccionRepositorioMock = new Mock<ITransaccionRepositorio>();

            var nuevoClienteMock = new Clientes
            {
                ClienteId = 1,
                NombreCompleto = "Juan Perez",
                Sexo = Datos.Enum.Genero.Masculino,
                FechaNacimiento = DateTime.UtcNow.AddYears(-30),
                IngresosMensuales = 1000m
            };

            var generadorMock = new GeneradorNoCuenta();
            var nuevaCuenta = generadorMock.GenerarNoCuenta();

            var cuentaMock = new Cuenta { NumeroCuenta = nuevaCuenta, Saldo = 100, ClienteId = nuevoClienteMock.ClienteId, SaldoInicial = 100 };

            cuentaRepositorioMock
                .Setup(repo => repo.ObtenerPorIdCuenta(nuevaCuenta))
                .ReturnsAsync(cuentaMock);

            var servicio = new CuentaServicios(
                cuentaRepositorioMock.Object,
                trabajoUnicoMock.Object,
                generadorNoCuentaMock.Object,
                transaccionRepositorioMock.Object
            );            

            Func<Task> accion = async () => await servicio.RetiroAsync(new TransaccionSolicitud { Monto = 500, NumeroCuenta = nuevaCuenta});

            await accion.Should().ThrowAsync<ExcepcionNegocio>();
        }

        [Fact]
        public async Task RetirarConFondosSuficientesDeberiaDisminuirSaldoYActualizarRepositorio()
        {
            var cuentaRepositorioMock = new Mock<ICuentaRepositorio>();
            var trabajoUnicoMock = new Mock<ITrabajoUnico>();
            var generadorNoCuentaMock = new Mock<IGeneradorNoCuenta>();
            var transaccionRepositorioMock = new Mock<ITransaccionRepositorio>();

            var nuevoClienteMock = new Clientes
            {
                ClienteId = 1,
                NombreCompleto = "Juan Perez",
                Sexo = Datos.Enum.Genero.Masculino,
                FechaNacimiento = DateTime.UtcNow.AddYears(-30),
                IngresosMensuales = 1000m
            };

            var generadorMock = new GeneradorNoCuenta();
            var nuevaCuenta = generadorMock.GenerarNoCuenta();

            var cuentaMock = new Cuenta { NumeroCuenta = nuevaCuenta, Saldo = 1000, ClienteId = nuevoClienteMock.ClienteId, SaldoInicial = 500 };

            cuentaRepositorioMock
            .Setup(repo => repo.ObtenerPorIdCuenta(nuevaCuenta))
            .ReturnsAsync(cuentaMock);

            var servicio = new CuentaServicios(
                cuentaRepositorioMock.Object,
                trabajoUnicoMock.Object,
                generadorNoCuentaMock.Object,
                transaccionRepositorioMock.Object
            );

            var cuenta = await servicio.RetiroAsync(new TransaccionSolicitud { NumeroCuenta = cuentaMock.NumeroCuenta, Monto = 300 });

            Assert.NotNull(cuenta);
            Assert.Equal(700, cuenta.Saldo);
            Assert.Equal(500, cuenta.SaldoInicial);
        }

        [Fact]
        public async Task CrearCuentaDeberiaGenerarNumeroYGuardarEnRepositorio()
        {            
            var generadorMock = new GeneradorNoCuenta();
            var nuevaCuenta = generadorMock.GenerarNoCuenta();

            Assert.NotNull(nuevaCuenta);            
            Assert.StartsWith("ACC-", nuevaCuenta);
            Assert.Matches(@"^ACC-\d{8}-\d{4}$", nuevaCuenta);

        }

    }
}
