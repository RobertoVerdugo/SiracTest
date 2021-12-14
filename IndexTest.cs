using System;
using Xunit;
using Test_Razor.Pages;
using Test_Razor.Models;
using SiracTest.Utilities;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SiracTest
{
    public class IndexTest
    {
        private readonly ICategoryService categoryService;
        private readonly UserManager<IdentityUser> userManager;
        [Fact]
        public void TestPaginarPublicacionesValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                int indice = 1;
                Publicacion pub = new Publicacion();
                Publicaciones.Add(pub);

                IEnumerable<Publicacion> result = index.PaginarPublicaciones(Publicaciones,indice);
                
                Assert.NotEmpty(result);
            }
            
        }
        [Fact]
        public void TestPaginarPublicacionesNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                int indice = -1; //indice negativo
                Publicacion pub = new Publicacion();
                Publicaciones.Add(pub);

                IEnumerable<Publicacion> result = index.PaginarPublicaciones(Publicaciones, indice);

                Assert.NotEmpty(result);

                indice = 15;//fuera de rango
                result = index.PaginarPublicaciones(Publicaciones, indice);

                Assert.NotEmpty(result);
            }

        }

        [Fact]
        public void TestFiltroEspecieValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                Filtro Filtro = new Filtro();

                string filtrado = "Perro";
                Filtro.especie = "1";// 1 Para perro

                Publicacion pub = new Publicacion();
                pub.especie = "Perro";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = null;
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.FiltrarPublicaciones(Publicaciones, Filtro);

                Assert.NotEmpty(resultList);
                foreach(var result in resultList)
                {
                    Assert.Equal(filtrado, result.especie);
                }
                resultList = index.FiltrarPublicaciones(new List<Publicacion>(), Filtro);
                Assert.Empty(resultList);
            }
            
        }
        [Fact]
        public void TestFiltroEspecieNoExiste()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                Filtro Filtro = new Filtro();

                Filtro.especie = "3";// 3 no existe

                Publicacion pub = new Publicacion();
                pub.especie = "Gato";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "Perro";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.FiltrarPublicaciones(Publicaciones, Filtro);

                Assert.Empty(resultList);
            }

        }
        [Fact]
        public void TestFiltroEspecieNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                Filtro Filtro = new Filtro();

                Filtro.especie = "1";// 1 para Perro
                Publicacion pub = new Publicacion();
                pub.especie = "Perico";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "1";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.FiltrarPublicaciones(Publicaciones, Filtro);

                Assert.Empty(resultList);
            }

        }

        [Fact]
        public void TestMultiFiltroValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                Filtro Filtro = new Filtro();

                string especie = "Perro";
                string genero = "Macho";
                Filtro.especie = "1";// 1 Para perro
                Filtro.genero = "Macho";

                Publicacion pub = new Publicacion();
                pub.especie = "Perro";
                pub.genero = "Hembra";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "Gato";
                pub2.genero = "Macho";
                Publicaciones.Add(pub2);

                Publicacion pub3 = new Publicacion();
                pub3.especie = "Perro";
                pub3.genero = "Macho";
                Publicaciones.Add(pub3);

                IEnumerable<Publicacion> test = Publicaciones;
                IEnumerable<Publicacion> resultList = index.FiltrarPublicaciones(Publicaciones, Filtro);

                Assert.NotEmpty(resultList);
                foreach (var result in resultList)
                {
                    Assert.Equal(especie, result.especie);
                    Assert.Equal(genero, result.genero);
                }

            }

        }
        [Fact]
        public void TestMultiFiltroNoExiste()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                Filtro Filtro = new Filtro();

                Filtro.especie = "3";// 3 no existe
                Filtro.genero = "Reptil";

                Publicacion pub = new Publicacion();
                pub.especie = "Perro";
                pub.genero = "Hembra";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "Gato";
                pub2.genero = "Macho";
                Publicaciones.Add(pub2);

                Publicacion pub3 = new Publicacion();
                pub3.especie = "Perro";
                pub3.genero = "Macho";
                Publicaciones.Add(pub3);

                IEnumerable<Publicacion> test = Publicaciones;
                IEnumerable<Publicacion> resultList = index.FiltrarPublicaciones(Publicaciones, Filtro);

                Assert.Empty(resultList);

            }

        }
        [Fact]
        public void TestMultiFiltroNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);
                Filtro Filtro = new Filtro();

                Filtro.especie = "1";// 1 para Perro
                Filtro.genero = "Macho";

                Publicacion pub = new Publicacion();
                pub.especie = "Perico";
                pub.genero = "Hembra";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "Gato";
                pub2.genero = "Reptil";
                Publicaciones.Add(pub2);

                Publicacion pub3 = new Publicacion();
                pub3.especie = "Perico";
                pub3.genero = "Reptil";
                Publicaciones.Add(pub3);

                IEnumerable<Publicacion> test = Publicaciones;
                IEnumerable<Publicacion> resultList = index.FiltrarPublicaciones(Publicaciones, Filtro);

                Assert.Empty(resultList);

            }

        }

        [Fact]
        public void TestOrdenValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);

                string orden = "Recomendados";
                string rut = "20.483.340-0";

                Publicacion pub = new Publicacion();
                pub.nombre = "z";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "A";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.OrdenarPublicaciones(Publicaciones, orden,rut);
                Assert.NotEmpty(resultList);
                var test = resultList.ToList();
                Assert.Equal(Publicaciones[0], test[0]);
                Assert.Equal(Publicaciones[1], test[1]);

                orden = "Nombre (A-Z)";
                resultList = index.OrdenarPublicaciones(Publicaciones, orden, rut);
                Assert.NotEmpty(resultList);
                test = resultList.ToList();
                Assert.Equal(Publicaciones[0], test[1]);
                Assert.Equal(Publicaciones[1], test[0]);

                resultList = index.OrdenarPublicaciones(new List<Publicacion>(), orden, rut);
                Assert.Empty(resultList);
            }

        }
        [Fact]
        public void TestOrdenNoExiste()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);

                string orden = "Color"; // no existe
                string rut = "20.483.340-0";

                Publicacion pub = new Publicacion();
                pub.nombre = "z";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "A";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.OrdenarPublicaciones(Publicaciones, orden,rut);
                Assert.NotEmpty(resultList);
                var test = resultList.ToList();
                Assert.Equal(Publicaciones[0], test[0]);
                Assert.Equal(Publicaciones[1], test[1]);

                orden = null;
                resultList = index.OrdenarPublicaciones(Publicaciones, orden,rut);
                Assert.NotEmpty(resultList);
                test = resultList.ToList();
                Assert.Equal(Publicaciones[0], test[0]);
                Assert.Equal(Publicaciones[1], test[1]);

                orden = "Recomendados";
                rut = "123"; //no existe
                resultList = index.OrdenarPublicaciones(Publicaciones, orden, rut);
                Assert.NotEmpty(resultList);
                test = resultList.ToList();
                Assert.Equal(Publicaciones[0], test[0]);
                Assert.Equal(Publicaciones[1], test[1]);
            }

        }

        [Fact]
        public void TestListaContenidoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);

                Preferencia pref = new Preferencia();
                Publicacion pub = new Publicacion();
                int puntos = 1;
                pub.nombre = "z";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "A";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.CrearListaContenido(Publicaciones,pref, puntos);
                Assert.NotEmpty(resultList);
                Assert.Equal(2, resultList.Count());
            }
        }
        [Fact]
        public void TestListaContenidoNoValido()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);

                Preferencia pref = new Preferencia();
                Publicacion pub = new Publicacion();
                int puntos = -1;
                pub.nombre = "z";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "A";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.CrearListaContenido(Publicaciones, pref, puntos);
                Assert.Empty(resultList);

                puntos = 0;
                resultList = index.CrearListaContenido(Publicaciones, pref, puntos);
                Assert.Empty(resultList);
            }
        }
        [Fact]
        public void TestListaContenidoVacio()
        {
            using (var db = new ApplicationDbContext(Utilities.Utilities.TestDbContextOptions()))
            {
                List<Publicacion> Publicaciones = new List<Publicacion>();
                IndexModel index = new IndexModel(db, categoryService, userManager);

                Preferencia pref = new Preferencia();
                Publicacion pub = new Publicacion();
                int puntos = 1;
                pub.nombre = "z";
                Publicaciones.Add(pub);

                Publicacion pub2 = new Publicacion();
                pub2.especie = "A";
                Publicaciones.Add(pub2);

                IEnumerable<Publicacion> resultList = index.CrearListaContenido(new List<Publicacion>(), null, puntos);
                Assert.Empty(resultList);

            }
        }

    }
}
