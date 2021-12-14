using System;
using Xunit;
using Test_Razor.Models;
using SiracTest.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SiracTest
{
    public class Db
    {
        [Fact]
        public void TestPublicacionValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Publicacion pub = new Publicacion();//contiene el id 1
                db.Add(pub);
                int id = 1;
                bool test = true;
                bool result = db.VerificarPublicacion(id);
                Assert.Equal(test, result);

            }
        }
        [Fact]
        public void TestPublicacionNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Publicacion pub = new Publicacion();//contiene el id 1
                db.Add(pub);
                int id = 2;
                bool test = false;
                bool result = db.VerificarPublicacion(id);
                Assert.Equal(test, result);

                id = 0;
                result = db.VerificarPublicacion(id);
                Assert.Equal(test, result);

                id = -1;
                result = db.VerificarPublicacion(id);
                Assert.Equal(test, result);

            }
        }


        [Fact]
        public void TestExisteUsuarioValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Usuario user = new Usuario();
                user.rut = "1";
                db.Add(user);
                string usuario = "1";
                bool test = true;
                bool result = db.ExisteUsuario(usuario);
                Assert.Equal(test, result);
            }
        }
        [Fact]
        public void TestExisteUsuarioNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Usuario user = new Usuario();
                user.rut = "1";
                db.Add(user);
                string usuario = "2";
                bool test = false;
                bool result = db.ExisteUsuario(usuario);
                Assert.Equal(test, result);

                usuario = null;
                result = db.ExisteUsuario(usuario);
                Assert.Equal(test, result);

                usuario ="a";
                result = db.ExisteUsuario(usuario);
                Assert.Equal(test, result);
            }
        }

        [Fact]
        public void TestUsuarioValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Usuario user= new Usuario();//contiene el id 1
                user.rut = "1";
                db.Add(user);
                string usuario= "1";
                string logued = "1";
                bool test = true;
                bool result = db.VerificarPropiedadUsuario(usuario,logued);
                Assert.Equal(test, result);
            }
        }
        [Fact]
        public void TestUsuarioNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Usuario user = new Usuario();
                user.rut = "1";
                db.Add(user);//contiene el id 1
                string usuario = "1";//usuario dueño
                string logued = "2";// usuario logueado
                bool test = false;
                bool result = db.VerificarPropiedadUsuario(usuario, logued);
                Assert.Equal(test, result);

                usuario = "2";
                logued = "1";
                result = db.VerificarPropiedadUsuario(usuario, logued);
                Assert.Equal(test, result);

                usuario = "2";
                logued = "2";
                result = db.VerificarPropiedadUsuario(usuario, logued);
                Assert.Equal(test, result);

            }
        }
        [Fact]
        public void TestUsuarioLetras()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Usuario user = new Usuario();
                user.rut = "1";
                db.Add(user);//contiene el id 1
                string usuario = "1";//usuario dueño
                string logued = "a";// usuario logueado
                bool test = false;
                bool result = db.VerificarPropiedadUsuario(usuario, logued);
                Assert.Equal(test, result);

                usuario = "a";
                logued = "1";
                result = db.VerificarPropiedadUsuario(usuario, logued);
                Assert.Equal(test, result);

                usuario = "a";
                logued = "a";
                result = db.VerificarPropiedadUsuario(usuario, logued);
                Assert.Equal(test, result);

            }
        }

        [Fact]
        public void TestEliminarValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Publicacion pub = new Publicacion();//contiene el id 1
                pub.filepath = "/test.jpg";
                db.Add(pub);
                int id = 1;
                bool test = true;
                bool result = db.EliminarPublicacion(id);
                Assert.Equal(test, result);
                pub = db.Publicacion.Find(id);
                Assert.Null(pub);

            }
        }
        [Fact]
        public void TestEliminarNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Publicacion pub = new Publicacion();//contiene el id 1
                db.Add(pub);
                int id = 0;
                bool test = false;
                bool result = db.EliminarPublicacion(id);
                Assert.Equal(test, result);
                pub = db.Publicacion.Find(1);
                Assert.NotNull(pub);

                id = 2;
                result = db.EliminarPublicacion(id);
                Assert.Equal(test, result);
                pub = db.Publicacion.Find(1);
                Assert.NotNull(pub);

                id = -1;
                result = db.EliminarPublicacion(id);
                Assert.Equal(test, result);
                pub = db.Publicacion.Find(1);
                Assert.NotNull(pub);

            }
        }

        [Fact]
        public void TestEliminarRepValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Reporte rep= new Reporte();//contiene el id 1
                db.Add(rep);
                int id = 0;
                bool test = false;
                bool result = db.EliminarReporte(id);
                Assert.Equal(test, result);

                rep = db.Reporte.Find(id);
                Assert.Null(rep);

                id = 0;
                result = db.EliminarReporte(id);
                Assert.Equal(test, result);
                rep = db.Reporte.Find(id);
                Assert.Null(rep);

                id = -1;
                result = db.EliminarReporte(id);
                Assert.Equal(test, result);
                rep = db.Reporte.Find(id);
                Assert.Null(rep);
            }
        }
        [Fact]
        public void TestEliminarRepNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                Reporte rep = new Reporte();//contiene el id 1
                db.Add(rep);
                int id = 1;
                bool test = true;
                bool result = db.EliminarReporte(id);
                Assert.Equal(test, result);

                rep = db.Reporte.Find(id);
                Assert.Null(rep);

            }
        }

        [Fact]
        public void TestDeleteFile()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                string ruta = "/test.txt";
                using FileStream fs = File.OpenWrite(ruta);
                var data = "falcon\nhawk\nforest\ncloud\nsky";
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                Assert.True(File.Exists(ruta));
                
                db.DeleteFile(ruta);

                Assert.False(File.Exists(ruta));

                ruta = "noexiste.txt";
                Assert.False(File.Exists(ruta));

                db.DeleteFile(ruta);

                Assert.False(File.Exists(ruta));

            }
        }
    }
}
