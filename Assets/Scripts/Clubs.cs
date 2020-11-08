using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clubs
{
    public enum ClubClass
    {
        WOOD,
        HYBRID,
        IRON,
        WEDGE,
        PUTTER,
        NONE
    }

    public enum ClubType
    {
        ONE_WOOD, TWO_WOOD, THREE_WOOD, FOUR_WOOD, FIVE_WOOD, SEVEN_WOOD, NINE_WOOD,
        TWO_HYBRID, THREE_HYBRID, FOUR_HYBRID, FIVE_HYBRID, SIX_HYBRID, SEVEN_HYBRID,
        ONE_IRON, TWO_IRON, THREE_IRON, FOUR_IRON, FIVE_IRON, SIX_IRON, SEVEN_IRON, EIGHT_IRON, NINE_IRON,
        PITCHING_WEDGE, GAP_WEDGE, SAND_WEDGE, LOB_WEDGE, FINAL_WEDGE,
        PUTTER
    }

    public static class ClubExtensions
    {
        public static ClubClass GetClubClass(this ClubType clubType)
        {
            switch (clubType)
            {
                case ClubType.ONE_WOOD:
                case ClubType.TWO_WOOD:
                case ClubType.THREE_WOOD:
                case ClubType.FOUR_WOOD:
                case ClubType.FIVE_WOOD:
                case ClubType.SEVEN_WOOD:
                case ClubType.NINE_WOOD:
                    return ClubClass.WOOD;
                case ClubType.TWO_HYBRID:
                case ClubType.THREE_HYBRID:
                case ClubType.FOUR_HYBRID:
                case ClubType.FIVE_HYBRID:
                case ClubType.SIX_HYBRID:
                case ClubType.SEVEN_HYBRID:
                    return ClubClass.HYBRID;
                case ClubType.ONE_IRON:
                case ClubType.TWO_IRON:
                case ClubType.THREE_IRON:
                case ClubType.FOUR_IRON:
                case ClubType.FIVE_IRON:
                case ClubType.SIX_IRON:
                case ClubType.SEVEN_IRON:
                case ClubType.EIGHT_IRON:
                case ClubType.NINE_IRON:
                    return ClubClass.IRON;
                case ClubType.PITCHING_WEDGE:
                case ClubType.GAP_WEDGE:
                case ClubType.SAND_WEDGE:
                case ClubType.LOB_WEDGE:
                case ClubType.FINAL_WEDGE:
                    return ClubClass.WEDGE;
                case ClubType.PUTTER:
                    return ClubClass.PUTTER;
                default:
                    return ClubClass.NONE;
            }
        }

        public static string GetClubName(this ClubType clubType)
        {
            switch (clubType)
            {
                case ClubType.ONE_WOOD:
                    return "1W";
                case ClubType.TWO_WOOD:
                    return "2W";
                case ClubType.THREE_WOOD:
                    return "3W";
                case ClubType.FOUR_WOOD:
                    return "4W";
                case ClubType.FIVE_WOOD:
                    return "5W";
                case ClubType.SEVEN_WOOD:
                    return "7W";
                case ClubType.NINE_WOOD:
                    return "9W";
                case ClubType.TWO_HYBRID:
                    return "2H";
                case ClubType.THREE_HYBRID:
                    return "3H";
                case ClubType.FOUR_HYBRID:
                    return "4H";
                case ClubType.FIVE_HYBRID:
                    return "5H";
                case ClubType.SIX_HYBRID:
                    return "6H";
                case ClubType.SEVEN_HYBRID:
                    return "7H";
                case ClubType.ONE_IRON:
                    return "1I";
                case ClubType.TWO_IRON:
                    return "2I";
                case ClubType.THREE_IRON:
                    return "3I";
                case ClubType.FOUR_IRON:
                    return "4I";
                case ClubType.FIVE_IRON:
                    return "5I";
                case ClubType.SIX_IRON:
                    return "6I";
                case ClubType.SEVEN_IRON:
                    return "7I";
                case ClubType.EIGHT_IRON:
                    return "8I";
                case ClubType.NINE_IRON:
                    return "9I";
                case ClubType.PITCHING_WEDGE:
                    return "PW";
                case ClubType.GAP_WEDGE:
                    return "GW";
                case ClubType.SAND_WEDGE:
                    return "SW";
                case ClubType.LOB_WEDGE:
                    return "LW";
                case ClubType.FINAL_WEDGE:
                    return "FW";
                case ClubType.PUTTER:
                    return "P";
                default:
                    return "N";
            }
        }

        public static bool IsNotPutter(this ClubType clubType) { return clubType != ClubType.PUTTER; }

        private static readonly Dictionary<ClubType, Polynomial> powerCurves = new Dictionary<ClubType, Polynomial>
        {
            { ClubType.ONE_WOOD, new Polynomial(0.010955812947717969f, 0.0f, 0.7097671078453911f, -1.6616721755539356f, 3.9700142421346083f, -3.477583100842982f, 1.4492145984687903f) },
            { ClubType.THREE_WOOD, new Polynomial(0.015045657379275101f, 0.0f, 0.8683886752449714f, -2.120065578619703f, 4.873124568192419f, -4.286511000690401f, 1.6502194792974836f) },
            { ClubType.FIVE_WOOD, new Polynomial(0.01664596125808193f, 0.0f, 0.9051099066369125f, -1.9551096382684583f, 4.1421544175473555f, -3.2406151105341596f, 1.1325077708889282f) },
            { ClubType.THREE_IRON, new Polynomial(0.018934338731739664f, 0.0f, 1.0375825755944612f, -2.6281533708602947f, 5.8955730475445485f, -5.265588678921648f, 1.9430300290389249f) },
            { ClubType.FOUR_IRON, new Polynomial(0.019608049503733738f, 0.0f, 1.1170170614314292f, -2.804496344521073f, 6.177415005640558f, -5.535193179039257f, 2.0271322135798626f) },
            { ClubType.FIVE_IRON, new Polynomial(0.021527712299569735f, 0.0f, 1.2706404300717833f, -3.513998236070226f, 7.822797273664134f, -7.241629354452408f, 2.640500306551611f) },
            { ClubType.SIX_IRON, new Polynomial(0.02403865070297051f, 0.0f, 1.358212795533941f, -3.596400568720183f, 7.619602446772283f, -6.750297015941989f, 2.3471586822275605f) },
            { ClubType.SEVEN_IRON, new Polynomial(0.023943578157348155f, 0.0f, 1.510059249743416f, -4.364879285509472f, 9.54554499180965f, -9.006092426127111f, 3.294387077329916f) },
            { ClubType.EIGHT_IRON, new Polynomial(0.02445672569497359f, 0.0f, 1.6842476881637167f, -4.989287446093307f, 10.744601596680388f, -10.083791391717059f, 3.6270612576888848f) },
            { ClubType.NINE_IRON, new Polynomial(0.03356001408100773f, 0.0f, 1.6941904034930213f, -4.816446977381638f, 10.221902598878962f, -9.56448574630949f, 3.434879284097034f) },
            { ClubType.PITCHING_WEDGE, new Polynomial(0.03047211196151589f, 0.0f, 1.9171017693702321f, -5.694064999415466f, 11.879281729767099f, -11.067499631088202f, 3.940044644683601f) },
            { ClubType.SAND_WEDGE, new Polynomial(0.0314934200414434f, 0.0f, 2.0821770725364925f, -6.032007055508311f, 12.067552990069887f, -10.968082955181604f, 3.8220423462106554f) },
            { ClubType.LOB_WEDGE, new Polynomial(0.04079742971334965f, 0.0f, 2.0170373467152505f, -4.869251096193187f, 8.691392558383043f, -7.246955926486148f, 2.369078175961751f) },
            { ClubType.PUTTER, new Polynomial(0.04662473119065946f, 0.0f, 2.9279364752905512f, -8.61820853632853f, 16.88138574196533f, -15.815120744434548f, 5.586768046089606f) }
        };

        public static float GetForce(this ClubType clubType, float power)
        {
            if (powerCurves.ContainsKey(clubType))
            {
                return powerCurves[clubType].Solve(power);
            }
            else
            {
                UnityEngine.Debug.Log(String.Format("ClubType {0} not found in powerCurve Dictionary!", clubType));
                return power;
            }
        }
    }
}
