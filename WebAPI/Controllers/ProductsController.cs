using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]  //route insanlar bize istek atarken nasıl ulaşsın
    [ApiController] //---->ATTRIBUTE   JAva da-->Annotation
    public class ProductsController : ControllerBase
    {
        //Looseşy coupled= gecşek bağımlılık.
        //naming convention

        //IoC Container -- Inversion of Control 
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            //_productService = new Productmanager(); normalde bu şekilde vermemiz lazım ki çözümleyebilsin.Elimizde somut referans yok çünkü

            //IoC Container -- Inversion of Control --Yani şunu yapıyor
            //IProductService productService şekilde yaptığımız IoC ile   bellekte new ProductManager ,new efproductdal  yani newleyip  sonra kullanmamızı sağlıyor
            //ASp web api bizim yerimize constructor içine tanımladığımız İnterface i yani IProductService IoC de bakıp ona karşılık gelen olduğu zaman onu kullanıyor
            //Bu yapı bunu sağlıyor kısaca
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Dependency chain --

            Thread.Sleep(5000);
            var result= _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
            
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
            
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }

    //Attribute bir class ile ilgili aslında bilgi verme onu imzalama yöntemidir
}
