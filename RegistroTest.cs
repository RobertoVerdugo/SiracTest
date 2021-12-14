using System;
using Xunit;
using Test_Razor.Pages;
using Test_Razor.Models;
using SiracTest.Utilities;
using Microsoft.AspNetCore.Identity;

namespace SiracTest
{
    public class RegistroTest
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        [Fact]
        public void TestFormatoRutValido()
        {
            RegistroModel registro = new RegistroModel(db,userManager,signInManager);

            string rut = "20.483.340-0";
            bool test = true;
            bool result = registro.verificarFormatoRut(rut);
            Assert.Equal(test,result);

            rut = "8.773.084-0";
            test = true;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

            rut = "8.073.084-0";
            test = true;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

        }
        [Fact]
        public void TestFormatoRutNoValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "20.483.340";
            bool test = false;
            bool result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

            rut = "0.483.340-0";
            test = false;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

            rut = "483.340-0";
            test = false;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

            rut = ".483.340-0";
            test = false;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

            rut = "00.483.340-0";
            test = false;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);

            rut = "20.483.340-b";
            test = false;
            result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestFormatoRutDigito()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "1";
            bool test = false;
            bool result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestFormatoRutVacio()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "";
            bool test = false;
            bool result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestFormatoRutNull()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = null;
            bool test = false;
            bool result = registro.verificarFormatoRut(rut);
            Assert.Equal(test, result);
        }

        [Fact]
        public void TestExistenciaRutValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "20.483.340-0";
            bool test = true;
            bool result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = "5.523.559-7";
            test = true;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = "8.773.084-0";
            test = true;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

        }
        [Fact]
        public void TestExistenciaRutNoValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "20.483.340-1";
            bool test = false;
            bool result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = "20.483.340";
            test = false;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = "0.483.340-0";
            test = false;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = "483.340-0";
            test = false;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = ".483.340-0";
            test = false;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);

            rut = "00.483.340-0";
            test = false;
            result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestExistenciaRutDigito()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "1";
            bool test = false;
            bool result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestExistenciaRutVacio()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = "";
            bool test = false;
            bool result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestExistenciaRutNull()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);

            string rut = null;
            bool test = false;
            bool result = registro.verificarExistenciaRut(rut);
            Assert.Equal(test, result);
        }
        
        [Fact]
        public void TestEdadPosibleValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);
            DateTime fecha =  new DateTime(2008, 3, 1);
            bool test = true;
            bool result = registro.verificarEdadPosible(fecha);
            Assert.Equal(test, result);
            
            fecha = DateTime.Now;
            result = registro.verificarEdadPosible(fecha);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestEdadPosibleNoValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);
            DateTime fecha = new DateTime(2022, 3, 1);
            bool test = false;
            bool result = registro.verificarEdadPosible(fecha);
            Assert.Equal(test, result);
        }

        [Fact]
        public void TestMayoEdadValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);
            DateTime fecha = new DateTime(2000, 3, 1);
            bool test = true;
            bool result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);

            fecha = DateTime.Today.AddYears(-18);
            result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);

            fecha = new DateTime(1900, 3, 1);
            result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);

            fecha = new DateTime(1800, 3, 1);
            result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);
        }
        [Fact]
        public void TestMayoEdadNoValido()
        {
            RegistroModel registro = new RegistroModel(db, userManager, signInManager);
            DateTime fecha = DateTime.Today;
            bool test = false;
            bool result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);

            fecha = new DateTime(2010, 3, 1);
            result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);

            fecha = new DateTime(2022, 3, 1);
            result = registro.verificarMayorDeEdad(fecha);
            Assert.Equal(test, result);
        }
    }
}
