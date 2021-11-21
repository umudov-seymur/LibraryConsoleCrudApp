using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryConsoleCrudApp.Business.Abstract
{
    interface IController
    {
        void Index();
        void Store();
        void Update();
        void Destroy();
    }
}
