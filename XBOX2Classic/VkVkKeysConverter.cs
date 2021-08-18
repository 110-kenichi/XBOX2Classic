using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Zanac.XBOX2Classic
{
    /// <summary>
    ///   <see cref="T:System.Windows.Forms.VkKeys" /> オブジェクトのさまざまな表現への変換や、さまざまな表現からの変換を実行するための <see cref="T:System.ComponentModel.TypeConverter" /> を提供します。</summary>
    /// <filterpriority>2</filterpriority>
    public class VkVkKeysConverter : TypeConverter, IComparer
    {
        private TypeConverter.StandardValuesCollection values;


        /// <summary>このコンバーターが、指定したコンテキストを使用して、指定した型のオブジェクトをコンバーターのネイティブな型に変換できるかどうかを示す値を返します。</summary>
        /// <returns>変換を実行できる場合は true。それ以外の場合は false。</returns>
        /// <param name="context">書式指定コンテキストを示す <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。これを使用すると、このコンバーターが呼び出されている環境に関する追加情報を抽出できます。このパラメーターまたはこのパラメーターのプロパティには、null を指定できます。</param>
        /// <param name="sourceType">変換対象の <see cref="T:System.Type" />。</param>
        /// <filterpriority>1</filterpriority>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
        }
        /// <summary>このコンバーターが、指定したコンテキストを使用して、指定した型のオブジェクトをコンバーターのネイティブな型に変換できるかどうかを示す値を返します。</summary>
        /// <returns>変換を実行できる場合は true。それ以外の場合は false。</returns>
        /// <param name="context">書式指定コンテキストを示す <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。これを使用すると、このコンバーターが呼び出されている環境に関する追加情報を抽出できます。このパラメーターまたはこのパラメーターのプロパティには、null を指定できます。</param>
        /// <param name="destinationType">変換後の <see cref="T:System.Type" />。</param>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
        }
        /// <summary>2 つのキー値が等しいかどうかを比較します。</summary>
        /// <returns>2 つのパラメーターの関係を示す整数。値型Condition負の整数。<paramref name="a" /> is less than <paramref name="b" />.0<paramref name="a" /> equals <paramref name="b" />.正の整数。<paramref name="a" /> is greater than <paramref name="b" />.</returns>
        /// <param name="a">比較する最初のキーを表す <see cref="T:System.Object" />。</param>
        /// <param name="b">比較する第 2 のキーを表す <see cref="T:System.Object" />。</param>
        /// <filterpriority>1</filterpriority>
        public int Compare(object a, object b)
        {
            return string.Compare(base.ConvertToString(a), base.ConvertToString(b), false, CultureInfo.InvariantCulture);
        }
        /// <summary>指定したオブジェクトをコンバーターのネイティブな型に変換します。</summary>
        /// <returns>変換後の <paramref name="value" /> を表すオブジェクト。</returns>
        /// <param name="context">書式指定コンテキストを示す ITypeDescriptorContext。これを使用すると、このコンバーターが呼び出されている環境に関する追加情報を抽出できます。このパラメーターまたはこのパラメーターのプロパティには、null を指定できます。</param>
        /// <param name="culture">ロケール情報を提供する CultureInfo オブジェクト。</param>
        /// <param name="value">変換対象のオブジェクト。</param>
        /// <exception cref="T:System.FormatException">無効なキーの組み合わせが指定されました。または無効なキー名が指定されました。</exception>
        /// <filterpriority>1</filterpriority>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            VkKeys VkKeys = VkKeys.None;
            if (value is string)
            {
                string text = ((string)value).Trim();
                if (text.Length == 0)
                {
                    return null;
                }
                string[] array = text.Split(new char[]
                {
                    '+'
                });
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].Trim();
                }
                for (int j = 0; j < array.Length; j++)
                {
                    if (!Enum.TryParse<VkKeys>(array[j], out VkKeys))
                    {
                        int val;
                        if (int.TryParse(array[j], out val))
                            VkKeys = (VkKeys)val;
                    }
                }
            }
            else if (value is int)
            {
                VkKeys = (VkKeys)value;
            }
            return VkKeys;
        }
        /// <summary>指定したオブジェクトを指定した型に変換します。</summary>
        /// <returns>変換後の <paramref name="value" /> を表す <see cref="T:System.Object" />。</returns>
        /// <param name="context">書式指定コンテキストを示す <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。これを使用すると、このコンバーターが呼び出されている環境に関する追加情報を抽出できます。このパラメーターまたはこのパラメーターのプロパティには、null を指定できます。</param>
        /// <param name="culture">ロケール情報を提供する <see cref="T:System.Globalization.CultureInfo" />。</param>
        /// <param name="value">変換対象の <see cref="T:System.Object" />。</param>
        /// <param name="destinationType">オブジェクトの変換後の <see cref="T:System.Type" />。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="destinationType" /> が null です。</exception>
        /// <filterpriority>1</filterpriority>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is VkKeys || value is int)
            {
                bool flag = destinationType == typeof(string);
                bool flag2 = false;
                if (!flag)
                {
                    flag2 = (destinationType == typeof(Enum[]));
                }
                if (flag | flag2)
                {
                    VkKeys VkKeys = (VkKeys)value;
                    bool flag3 = false;
                    ArrayList arrayList = new ArrayList();
                    VkKeys VkKeys2 = VkKeys & VkKeys.Modifiers;
                    VkKeys VkKeys4 = VkKeys & VkKeys.KeyCode;
                    bool flag4 = false;
                    if (flag3 & flag)
                    {
                        arrayList.Add("+");
                    }
                    if (!flag4 && Enum.IsDefined(typeof(VkKeys), (int)VkKeys4))
                    {
                        if (flag)
                        {
                            arrayList.Add(VkKeys4.ToString());
                        }
                        else
                        {
                            arrayList.Add(VkKeys4);
                        }
                    }
                    if (flag)
                    {
                        StringBuilder stringBuilder = new StringBuilder(32);
                        foreach (string value2 in arrayList)
                        {
                            stringBuilder.Append(value2);
                        }
                        return stringBuilder.ToString();
                    }
                    return (Enum[])arrayList.ToArray(typeof(Enum));
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        /// <summary>書式指定コンテキストが指定されている場合に、この型コンバーターが変換対象とするデータ型の標準値のコレクションを返します。</summary>
        /// <returns>有効値の標準セットを保持している <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" />。データ型が値の標準セットをサポートしていない場合は空になる場合があります。</returns>
        /// <param name="context">書式指定コンテキストを提供する <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。これを使用すると、このコンバーターが呼び出される環境に関する追加情報を取得できます。このパラメーターまたはこのパラメーターのプロパティには、null を指定できます。</param>
        /// <filterpriority>1</filterpriority>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (this.values == null)
            {
                this.values = new TypeConverter.StandardValuesCollection(Enum.GetNames(typeof(VkKeys)));
            }
            return this.values;
        }
        /// <summary>指定した <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> を使用して、GetStandardValues から返される標準値のリストが除外リストであるかどうかを判断します。</summary>
        /// <returns>
        ///   <see cref="Overload:System.Windows.Forms.VkKeysConverter.GetStandardValues" /> から返されたコレクションが、有効値の除外リストである場合は true。他にも有効値がある場合は false。このメソッドの既定の実装は常に false を返します。</returns>
        /// <param name="context">書式指定コンテキスト。このオブジェクトを使用すると、このコンバーターが呼び出されている環境についての追加情報を抽出できます。この値は null になる場合があるため、常に確認してください。また、コンテキスト オブジェクト上のプロパティも null を返す場合があります。</param>
        /// <filterpriority>1</filterpriority>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
        /// <summary>リストから選択できる標準値セットをこのオブジェクトがサポートしているかどうかを示す値を取得します。</summary>
        /// <returns>常に true を返します。</returns>
        /// <param name="context">書式指定コンテキストを示す <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。これを使用すると、このコンバーターが呼び出されている環境に関する追加情報を抽出できます。このパラメーターまたはこのパラメーターのプロパティには、null を指定できます。</param>
        /// <filterpriority>1</filterpriority>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
