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
    }
}
