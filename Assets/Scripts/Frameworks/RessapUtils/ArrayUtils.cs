using System.Collections.Generic;

namespace Ressap.Utils {
    public static class ArrayUtils {

        #region arr[]

        /// <summary>
        /// Fill list with the same element.
        /// </summary>
        public static void Fill<T>(this T[] arr, T element = default(T)) {
            if (null == arr || arr.Length <= 0) {
                return;
            }
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = element;
            }
        }

        #endregion


        # region List

        /// <summary>
        /// Returns a list only has single element.
        /// </summary>
        public static List<T> SingletonList<T>(T element = default(T)) {
            return new List<T>() { element };
        }

        /// <summary>
        /// Returns more readable string for the list.
        /// </summary>
        public static string List2String<T>(List<T> list, string delimiter, string prefix, string suffix) {
            return List2String(list.ToArray(), delimiter, prefix, suffix);
        }
        public static string List2String<T>(T[] arr, string delimiter, string prefix, string suffix) {
            if (null == arr) {
                return "null";
            }

            if (arr.Length <= 0) {
                return "empty";
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(prefix);

            sb.Append(arr[0].ToString());

            for (int i = 1; i < arr.Length; i++) {
                sb.Append(delimiter);
                sb.Append(arr[i].ToString());
            }

            sb.Append(suffix);

            return sb.ToString();
        }
        /// <summary>
        /// Returns more readable string for the list.
        /// </summary>
        public static string List2String<T>(List<T> list) {
            return List2String(list, ", ", "[", "]");
        }
        public static string List2String<T>(T[] arr) {
            return List2String(arr, ", ", "[", "]");
        }
        /// <summary>
        /// Returns more readable string for the list.
        /// </summary>
        public static string List2String<T>(List<T> list, string delimiter) {
            return List2String(list, delimiter, "[", "]");
        }
        public static string List2String<T>(T[] arr, string delimiter) {
            return List2String(arr, delimiter, "[", "]");
        }

        #endregion
    }
}