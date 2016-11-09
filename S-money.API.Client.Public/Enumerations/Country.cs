using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smoney.API.Client.Enumerations
{
    /// <summary>
    /// Selon la norme ISO 3166-1 alpha-2 http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
    /// Note : faudrait remettre les noms en anglais...
    /// </summary>
    public enum Country
    {
        None = 0,

        [EnumMember(Value = "FR")]
        France = 1,

        [EnumMember(Value = "DE")]
        Allemagne = 2,

        [EnumMember(Value = "AT")]
        Autriche = 3,

        [EnumMember(Value = "BE")]
        Belgique = 4,

        [EnumMember(Value = "BG")]
        Bulgarie = 5,

        [EnumMember(Value = "CY")]
        Chypre = 6,

        [EnumMember(Value = "DK")]
        Danemark = 7,

        [EnumMember(Value = "ES")]
        Espagne = 8,

        [EnumMember(Value = "EE")]
        Estonie = 9,

        [EnumMember(Value = "FI")]
        Finlande = 10,

        [EnumMember(Value = "GR")]
        Grèce = 11,

        [EnumMember(Value = "HU")]
        Hongrie = 12,

        [EnumMember(Value = "IE")]
        Irlande = 13,

        [EnumMember(Value = "IS")]
        Islande = 14,

        [EnumMember(Value = "IT")]
        Italie = 15,

        [EnumMember(Value = "LV")]
        Lettonie = 16,

        [EnumMember(Value = "LI")]
        Liechtenstein = 17,

        [EnumMember(Value = "LT")]
        Lituanie = 18,

        [EnumMember(Value = "LU")]
        Luxembourg = 19,

        [EnumMember(Value = "MT")]
        Malte = 20,

        [EnumMember(Value = "NO")]
        Norvège = 21,

        [EnumMember(Value = "NL")]
        PaysBas = 22,

        [EnumMember(Value = "PL")]
        Pologne = 23,

        [EnumMember(Value = "PT")]
        Portugal = 24,

        [EnumMember(Value = "CZ")]
        RépubliqueTchèque = 25,

        [EnumMember(Value = "RO")]
        Roumanie = 26,

        [EnumMember(Value = "GB")]
        RoyaumeUni = 27,

        [EnumMember(Value = "SK")]
        Slovaquie = 28,

        [EnumMember(Value = "SI")]
        Slovénie = 29,

        [EnumMember(Value = "SE")]
        Suède = 30,

        [EnumMember(Value = "CH")]
        Suisse = 32,

        [EnumMember(Value = "ZA")]
        AfriqueDuSud = 33,

        [EnumMember(Value = "AU")]
        Australie = 34,

        [EnumMember(Value = "BR")]
        Brésil = 35,

        [EnumMember(Value = "CA")]
        Canada = 36,

        [EnumMember(Value = "KR")]
        CoréeDuSud = 37,

        [EnumMember(Value = "US")]
        EtatsUnis = 38,

        [EnumMember(Value = "HK")]
        HongKong = 39,

        [EnumMember(Value = "IN")]
        Inde = 40,

        [EnumMember(Value = "JP")]
        Japon = 41,

        [EnumMember(Value = "MX")]
        Mexique = 42,

        [EnumMember(Value = "SG")]
        Singapour = 43,

        Other = 31
    }
}