﻿using Game.Forum.Infrastructure.Steam.CustomTypes;
using Game.Forum.Infrastructure.Steam.Enumerations;
using Game.Forum.Infrastructure.Steam.Services.Abstraction;
using System.Globalization;

namespace Game.Forum.Infrastructure.Steam.Services.Implementation
{
    public class CultureService : ICultureService
    {
        Dictionary<Culture, CultureValue> CultureList = new Dictionary<Culture, CultureValue>();

        public CultureService()
        {
            //Dil listesine elemanlar ekleniyor.
            //Yeni dil olursa buraya ekleme yap.
            CultureList.Add(Culture.tr_TR, new CultureValue { CultureName = "turkish", CultureShortName = "tr", CultureInfo = new CultureInfo("TR-tr") });
            CultureList.Add(Culture.en_US, new CultureValue { CultureName = "english", CultureShortName = "us", CultureInfo = new CultureInfo("EN-us") });
        }

        public CultureValue GetCultureValue(Culture culture)
        {
            return CultureList[culture];
        }
    }
}
