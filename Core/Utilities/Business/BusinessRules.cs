using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {

        public static IResult Run(params IResult[] logics) //c# da params ile birden fazla parametreleri virgüllü şekilde ayırarak dizi formatında atabiliyoruz
        {
            foreach (var logic in logics)
            {
                //Yukarıda params ile parametre ile gönderdiğimiz iş kurallarını business a gönder diyoruz
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }

        //public static List<IResult> Run(params IResult[] logics) //c# da params ile birden fazla parametreleri virgüllü şekilde ayırarak dizi formatında atabiliyoruz
        //{
        //    List<IResult> errorResult = new List<IResult>();
        //    foreach (var logic in logics)
        //    {
        //        //Yukarıda params ile parametre ile gönderdiğimiz iş kurallarını business a gönder diyoruz
        //        if (!logic.Success)
        //        {
        //            errorResult.Add(logic);
        //        }
        //    }
        //    return errorResult;
        //}
    }
}
