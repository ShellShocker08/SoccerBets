using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Soccer.Common.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);

    }
}
