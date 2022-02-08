using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        //Genel bağımlılıkları yüklemek için
        void Load(IServiceCollection serviceCollection);
    }
}
