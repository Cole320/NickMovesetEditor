// Ooh, live the dream in a time machine
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace NickMovesetEditor.Windows
{
    public class Helper
    {
        // These two functions are god awful,
        // my plan is to write them and then never touch them again
        public static string GetActionValue(JObject statesJson, List<string> actionPath, dynamic valueKey)
        {
            // 5 = Action
            // 8 = SubAction
            // 10 = SubSubAction
            // TODO: add some type of check to prevent crashing here

            if (int.TryParse(valueKey, out int valueKeyInt))
            {
                valueKey = valueKeyInt;
            }
            
            // Selected Action Path
            if (actionPath.Count == 5)
            {
                return statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][valueKey].ToString();
            }

            // Selected SubAction Path
            if (actionPath.Count == 8)
            {
                Debug.WriteLine(actionPath.ToString());
                return statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][actionPath[5]][actionPath[6]][int.Parse(actionPath[7])][valueKey].ToString();
            }
            
            // Selected SubSubAction Path
            if (actionPath.Count == 10)
            {
                return statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][actionPath[5]][actionPath[6]][int.Parse(actionPath[7])][actionPath[8]][int.Parse(actionPath[9])][valueKey].ToString();
            }
            
            if (actionPath.Count == 11)
            {
                return statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][actionPath[5]][actionPath[6]][int.Parse(actionPath[7])][actionPath[8]][int.Parse(actionPath[9])][actionPath[10]][valueKey].ToString();
            }
            return "L";
        }

        public static void SetActionValue(ref JObject statesJson, List<string> actionPath, string valueKey, string newValue)
        {
            // TODO: Fix this
            // if (int.TryParse(valueKey, out int valueKeyInt))
            // {
            //     dynamic valueKeyDynamic = valueKeyInt;
            // }
            //
            // if (int.TryParse(newValue, out int newValueInt))
            // {
            //     dynamic newValueDynamic = newValueInt;
            // }
            
            // Selected Action Path
            if (actionPath.Count == 5)
            {
                statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][valueKey] = newValue;
            }

            // Selected SubAction Path
            if (actionPath.Count == 8)
            {
                statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][actionPath[5]][actionPath[6]][actionPath[7]][valueKey] = newValue;
            }
            
            // Selected SubSubAction Path
            if (actionPath.Count == 10)
            {
                statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][actionPath[5]][actionPath[6]][int.Parse(actionPath[7])][actionPath[8]][int.Parse(actionPath[9])][valueKey] = newValue;
            }
            
            // Hack used for SetFloatId rn
            if (actionPath.Count == 11)
            {
                statesJson[actionPath[0]][int.Parse(actionPath[1])][actionPath[2]][actionPath[3]][int.Parse(actionPath[4])][actionPath[5]][actionPath[6]][int.Parse(actionPath[7])][actionPath[8]][int.Parse(actionPath[9])][actionPath[10]][valueKey] = newValue;
            }
        }

        // TODO: implement this in static helper class
        public static string GetActionTid(JObject statesJson, List<string> actionPath)
        {
            // This is unbearably bad
            if (actionPath.Count == 5)
            {
                return JObject.Parse(GetActionValue(statesJson, actionPath, "Action"))["TID"].ToString();
            }

            if (actionPath.Count == 8 || actionPath.Count == 10)
            {
                return GetActionValue(statesJson, actionPath, "TID");
            }

            return "";
        }

        public static bool IsActionRoot(List<string> actionPath)
        {
            return actionPath.Count == 5;
        }
    }
}