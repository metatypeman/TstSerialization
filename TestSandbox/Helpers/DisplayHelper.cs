﻿using System.Collections;
using System.Text;

namespace TestSandbox.Helpers
{
    public static class DisplayHelper
    {
        public const uint IndentationStep = 4u;

        public static string Spaces(uint n)
        {
            if (n == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            for (var i = 0; i < n; i++)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public static string GetDefaultToStringInformation(this IObjectToString targetObject, uint n)
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var sb = new StringBuilder();
            var nameOfType = targetObject.GetType().FullName;
            sb.AppendLine($"{spaces}Begin {nameOfType}");
            sb.Append(targetObject.PropertiesToString(nextN));
            sb.AppendLine($"{spaces}End {nameOfType}");
            return sb.ToString();
        }

        public static void PrintObjProp(this StringBuilder sb, uint n, string propName, IObjectToString obj)
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;

            if (obj == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                sb.Append(obj.ToString(nextN));
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintPODValue(this StringBuilder sb, uint n, string value)
        {
            var spaces = Spaces(n);

            var linesList = value.Replace('\r', ' ').Split('\n');

            foreach (var line in linesList)
            {
                sb.AppendLine($"{spaces}{line}");
            }
        }

        public static void PrintPODProp(this StringBuilder sb, uint n, string propName, string value)
        {
            var spaces = Spaces(n);

            if (string.IsNullOrWhiteSpace(value))
            {
                sb.AppendLine($"{spaces}{propName} = ");
                return;
            }

            var linesList = value.Replace('\r', ' ').Split('\n');

            if (linesList.Length == 1)
            {
                sb.AppendLine($"{spaces}{propName} = {value}");
                return;
            }

            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);

            sb.AppendLine($"{spaces}Begin {propName}");

            foreach (var line in linesList)
            {
                sb.AppendLine($"{nextNSpaces}{line}");
            }

            sb.AppendLine($"{spaces}End {propName}");
        }

        public static void PrintPODList<T>(this StringBuilder sb, uint n, string propName, IEnumerable<T> items)
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}{propName} = Begin List");
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}{item}");
                }
                sb.AppendLine($"{spaces}End List");
            }
        }

        public static void PrintObjListProp<T>(this StringBuilder sb, uint n, string propName, IEnumerable<T> items)
            where T : IObjectToString
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                foreach (var item in items)
                {
                    if (item == null)
                    {
                        sb.AppendLine($"{nextNSpaces}NULL");
                    }
                    else
                    {
                        sb.Append(item.ToString(nextN));
                    }
                }
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintValueTypesListProp<T>(this StringBuilder sb, uint n, string propName, IEnumerable<T> items)
            where T : struct
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}{item}");
                }
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintObjDict_1_Prop<K, V>(this StringBuilder sb, uint n, string propName, IEnumerable<KeyValuePair<K, V>> items)
            where K : IObjectToString
            where V : IObjectToString
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);
            var nextNextNextN = nextNextN + IndentationStep;

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Beign Key");
                    sb.Append(item.Key.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Key");
                    sb.AppendLine($"{nextNextNSpaces}Begin Value");
                    sb.Append(item.Value.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Value");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintObjDict_2_Prop<K, V>(this StringBuilder sb, uint n, string propName, IDictionary<K, V> items)
            where K : struct
            where V : IObjectToString
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);
            var nextNextNextN = nextNextN + IndentationStep;

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Key = {item.Key}");
                    sb.AppendLine($"{nextNextNSpaces}Begin Value");
                    sb.Append(item.Value.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Value");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintObjDict_3_Prop<V>(this StringBuilder sb, uint n, string propName, IEnumerable<KeyValuePair<string, V>> items)
            where V : IObjectToString
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);
            var nextNextNextN = nextNextN + IndentationStep;

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Key = {item.Key}");
                    sb.AppendLine($"{nextNextNSpaces}Begin Value");
                    sb.Append(item.Value.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Value");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintPODDictProp<K, V>(this StringBuilder sb, uint n, string propName, IEnumerable<KeyValuePair<K, V>> items)
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);

            if (items == null)
            {
                sb.AppendLine($"{spaces}{propName} = NULL");
            }
            else
            {
                sb.AppendLine($"{spaces}Begin {propName}");
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Key = {item.Key}");
                    sb.AppendLine($"{nextNextNSpaces}Value = {item.Value}");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
                sb.AppendLine($"{spaces}End {propName}");
            }
        }

        public static void PrintExisting(this StringBuilder sb, uint n, string propName, object value)
        {
            var spaces = Spaces(n);
            var mark = GetPrintExistingMark(value);
            sb.AppendLine($"{spaces}{propName} = {mark}");
        }

        private const string NoMark = "No";
        private const string YesMark = "Yes";

        private static string GetPrintExistingMark(object value)
        {
            if (value == null)
            {
                return NoMark;
            }

            var str = value as string;

            if (str != null)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return NoMark;
                }

                return YesMark;
            }

            var enumerable = value as IEnumerable;

            if (enumerable != null)
            {
                if (enumerable.GetEnumerator().MoveNext())
                {
                    return YesMark;
                }

                return NoMark;
            }

            return YesMark;
        }

        public static string PrintPODListProp<T>(this StringBuilder sb, uint n, string propName, IEnumerable<T> items)
        {
            var spaces = Spaces(n);
            var nextN = n + IndentationStep;
            var nextNSpaces = Spaces(nextN);

            if (items == null)
            {
                return $"{spaces}{propName}NULL";
            }

            sb.AppendLine($"{spaces}Begin {propName}");
            if (!items.IsNullOrEmpty())
            {
                foreach (var item in items)
                {
                    if (item == null)
                    {
                        sb.AppendLine($"{nextNSpaces}NULL");
                        continue;
                    }
                    sb.AppendLine($"{nextNSpaces}{item}");
                }
            }
            sb.AppendLine($"{spaces}End {propName}");
            return sb.ToString();
        }

        public static string WritePODListToString<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                return "NULL";
            }

            var sb = new StringBuilder();
            sb.AppendLine("Begin List");
            if (!items.IsNullOrEmpty())
            {
                foreach (var item in items)
                {
                    if (item == null)
                    {
                        sb.AppendLine("NULL");
                        continue;
                    }
                    sb.AppendLine(item.ToString());
                }
            }
            sb.AppendLine("End List");
            return sb.ToString();
        }

        public static string WriteListToString<T>(this IEnumerable<T> items)
            where T : IObjectToString
        {
            if (items == null)
            {
                return "NULL";
            }

            var sb = new StringBuilder();
            sb.AppendLine("Begin List");
            if (!items.IsNullOrEmpty())
            {
                foreach (var item in items)
                {
                    sb.Append(item.ToString(4u));
                }
            }
            sb.AppendLine("End List");
            return sb.ToString();
        }

        public static string WriteDict_1_ToString<K, V>(this IDictionary<K, V> items)
            where K : IObjectToString
            where V : IObjectToString
        {
            if (items == null)
            {
                return "NULL";
            }

            var nextN = IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);
            var nextNextNextN = nextNextN + IndentationStep;

            var sb = new StringBuilder();
            sb.AppendLine("Begin Dictionary");

            if (!items.IsNullOrEmpty())
            {
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Beign Key");
                    sb.Append(item.Key.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Key");
                    sb.AppendLine($"{nextNextNSpaces}Begin Value");
                    sb.Append(item.Value.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Value");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
            }

            sb.AppendLine("End Dictionary");
            return sb.ToString();
        }

        public static string WriteDict_2_ToString<K, V>(this IDictionary<K, V> items)
            where K : struct
            where V : IObjectToString
        {
            if (items == null)
            {
                return "NULL";
            }

            var nextN = IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);
            var nextNextNextN = nextNextN + IndentationStep;

            var sb = new StringBuilder();
            sb.AppendLine("Begin Dictionary");

            if (!items.IsNullOrEmpty())
            {
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Key = {item.Key}");
                    sb.AppendLine($"{nextNextNSpaces}Begin Value");
                    sb.Append(item.Value.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Value");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
            }

            sb.AppendLine("End Dictionary");
            return sb.ToString();
        }

        public static string WriteDict_3_ToString<V>(this IDictionary<string, V> items)
            where V : IObjectToString
        {
            if (items == null)
            {
                return "NULL";
            }

            var nextN = IndentationStep;
            var nextNSpaces = Spaces(nextN);
            var nextNextN = nextN + IndentationStep;
            var nextNextNSpaces = Spaces(nextNextN);
            var nextNextNextN = nextNextN + IndentationStep;

            var sb = new StringBuilder();
            sb.AppendLine("Begin Dictionary");

            if (!items.IsNullOrEmpty())
            {
                foreach (var item in items)
                {
                    sb.AppendLine($"{nextNSpaces}Begin Item");
                    sb.AppendLine($"{nextNextNSpaces}Key = {item.Key}");
                    sb.AppendLine($"{nextNextNSpaces}Begin Value");
                    sb.Append(item.Value.ToString(nextNextNextN));
                    sb.AppendLine($"{nextNextNSpaces}End Value");
                    sb.AppendLine($"{nextNSpaces}End Item");
                }
            }

            sb.AppendLine("End Dictionary");
            return sb.ToString();
        }
    }
}
