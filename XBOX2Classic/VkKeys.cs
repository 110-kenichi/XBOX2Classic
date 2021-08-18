using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Zanac.XBOX2Classic
{
    /// <summary>キー コードと修飾子を指定します。</summary>
    /// <filterpriority>2</filterpriority>
    [Flags, TypeConverter(typeof(VkVkKeysConverter)), ComVisible(true)]
    public enum VkKeys
    {
        /// <summary>キー値からキー コードを抽出するビット マスク。</summary>
        KeyCode = 65535,
        /// <summary>キー値から修飾子を抽出するビット マスク。</summary>
        Modifiers = -65536,
        /// <summary>キー入力なし</summary>
        None = 0,
        /// <summary>マウスの左ボタン</summary>
        LButton = 1,
        /// <summary>マウスの右ボタン</summary>
        RButton = 2,
        /// <summary>Cancel キー</summary>
        Cancel = 3,
        /// <summary>マウスの中央ボタン (3 ボタン マウスの場合)</summary>
        MButton = 4,
        /// <summary>x マウスの 1 番目のボタン (5 ボタン マウスの場合)</summary>
        XButton1 = 5,
        /// <summary>x マウスの 2 番目のボタン (5 ボタン マウスの場合)</summary>
        XButton2 = 6,
        /// <summary>BackSpace キー</summary>
        Back = 8,
        /// <summary>The TAB key.</summary>
        Tab = 9,
        /// <summary>ライン フィード キー</summary>
        LineFeed = 10,
        /// <summary>Clear キー</summary>
        Clear = 12,
        /// <summary>Return キー</summary>
        Return = 13,
        /// <summary>Enter キー</summary>
        Enter = 13,
        /// <summary>Shift キー</summary>
        ShiftKey = 16,
        /// <summary>The CTRL key.</summary>
        ControlKey = 17,
        /// <summary>Alt キー</summary>
        Menu = 18,
        /// <summary>Pause キー</summary>
        Pause = 19,
        /// <summary>The CAPS LOCK key.</summary>
        Capital = 20,
        /// <summary>The CAPS LOCK key.</summary>
        CapsLock = 20,
        /// <summary>IME かなモード キー</summary>
        KanaMode = 21,
        /// <summary>IME ハングル モード キー(互換性を保つために保持されています。HangulMode を使用します)</summary>
        HanguelMode = 21,
        /// <summary>IME ハングル モード キー</summary>
        HangulMode = 21,
        /// <summary>IME Junja モード キー</summary>
        JunjaMode = 23,
        /// <summary>IME Final モード キー</summary>
        FinalMode = 24,
        /// <summary>IME Hanja モード キー</summary>
        HanjaMode = 25,
        /// <summary>IME 漢字モード キー</summary>
        KanjiMode = 25,
        /// <summary>The ESC key.</summary>
        Escape = 27,
        /// <summary>IME 変換キー</summary>
        IMEConvert = 28,
        /// <summary>IME 無変換キー</summary>
        IMENonconvert = 29,
        /// <summary>IME Accept キー (<see cref="F:System.Windows.Forms.Keys.IMEAceept" /> の代わりに使用します)</summary>
        IMEAccept = 30,
        /// <summary>IME Accept キー互換性を維持するために残されています。代わりに <see cref="F:System.Windows.Forms.Keys.IMEAccept" /> を使用してください。</summary>
        IMEAceept = 30,
        /// <summary>IME モード変更キー</summary>
        IMEModeChange = 31,
        /// <summary>Space キー</summary>
        Space = 32,
        /// <summary>PageUp キー</summary>
        Prior = 33,
        /// <summary>PageUp キー</summary>
        PageUp = 33,
        /// <summary>The PAGE DOWN key.</summary>
        Next = 34,
        /// <summary>The PAGE DOWN key.</summary>
        PageDown = 34,
        /// <summary>The END key.</summary>
        End = 35,
        /// <summary>The HOME key.</summary>
        Home = 36,
        /// <summary>← キー</summary>
        Left = 37,
        /// <summary>↑ キー</summary>
        Up = 38,
        /// <summary>→ キー</summary>
        Right = 39,
        /// <summary>↓ キー</summary>
        Down = 40,
        /// <summary>Select キー</summary>
        Select = 41,
        /// <summary>Print キー</summary>
        Print = 42,
        /// <summary>Execute キー</summary>
        Execute = 43,
        /// <summary>PrintScreen キー</summary>
        Snapshot = 44,
        /// <summary>PrintScreen キー</summary>
        PrintScreen = 44,
        /// <summary>The INS key.</summary>
        Insert = 45,
        /// <summary>The DEL key.</summary>
        Delete = 46,
        /// <summary>The HELP key.</summary>
        Help = 47,
        /// <summary>The 0 key.</summary>
        D0 = 48,
        /// <summary>The 1 key.</summary>
        D1 = 49,
        /// <summary>The 2 key.</summary>
        D2 = 50,
        /// <summary>The 3 key.</summary>
        D3 = 51,
        /// <summary>The 4 key.</summary>
        D4 = 52,
        /// <summary>The 5 key.</summary>
        D5 = 53,
        /// <summary>The 6 key.</summary>
        D6 = 54,
        /// <summary>The 7 key.</summary>
        D7 = 55,
        /// <summary>The 8 key.</summary>
        D8 = 56,
        /// <summary>The 9 key.</summary>
        D9 = 57,
        /// <summary>A キー</summary>
        A = 65,
        /// <summary>B キー</summary>
        B = 66,
        /// <summary>C キー</summary>
        C = 67,
        /// <summary>D キー</summary>
        D = 68,
        /// <summary>E キー</summary>
        E = 69,
        /// <summary>F キー</summary>
        F = 70,
        /// <summary>G キー</summary>
        G = 71,
        /// <summary>H キー</summary>
        H = 72,
        /// <summary>I キー</summary>
        I = 73,
        /// <summary>J キー</summary>
        J = 74,
        /// <summary>K キー</summary>
        K = 75,
        /// <summary>L キー</summary>
        L = 76,
        /// <summary>M キー</summary>
        M = 77,
        /// <summary>N キー</summary>
        N = 78,
        /// <summary>O キー</summary>
        O = 79,
        /// <summary>P キー</summary>
        P = 80,
        /// <summary>Q キー</summary>
        Q = 81,
        /// <summary>R キー</summary>
        R = 82,
        /// <summary>S キー</summary>
        S = 83,
        /// <summary>T キー</summary>
        T = 84,
        /// <summary>U キー</summary>
        U = 85,
        /// <summary>V キー</summary>
        V = 86,
        /// <summary>W キー</summary>
        W = 87,
        /// <summary>X キー</summary>
        X = 88,
        /// <summary>Y キー</summary>
        Y = 89,
        /// <summary>Z キー</summary>
        Z = 90,
        /// <summary>左の Windows ロゴ キー (Microsoft Natural Keyboard)</summary>
        LWin = 91,
        /// <summary>右の Windows ロゴ キー (Microsoft Natural Keyboard)</summary>
        RWin = 92,
        /// <summary>アプリケーション キー (Microsoft Natural Keyboard)</summary>
        Apps = 93,
        /// <summary>コンピューターのスリープ キー</summary>
        Sleep = 95,
        /// <summary>The 0 key on the numeric keypad.</summary>
        NumPad0 = 96,
        /// <summary>The 1 key on the numeric keypad.</summary>
        NumPad1 = 97,
        /// <summary>数値キーパッドの 2 キー</summary>
        NumPad2 = 98,
        /// <summary>数値キーパッドの 3 キー</summary>
        NumPad3 = 99,
        /// <summary>数値キーパッドの 4 キー</summary>
        NumPad4 = 100,
        /// <summary>数値キーパッドの 5 キー</summary>
        NumPad5 = 101,
        /// <summary>数値キーパッドの 6 キー</summary>
        NumPad6 = 102,
        /// <summary>数値キーパッドの 7 キー</summary>
        NumPad7 = 103,
        /// <summary>The 8 key on the numeric keypad.</summary>
        NumPad8 = 104,
        /// <summary>The 9 key on the numeric keypad.</summary>
        NumPad9 = 105,
        /// <summary>乗算記号 (*) キー</summary>
        Multiply = 106,
        /// <summary>Add キー</summary>
        Add = 107,
        /// <summary>区切り記号キー</summary>
        Separator = 108,
        /// <summary>減算記号 (-) キー</summary>
        Subtract = 109,
        /// <summary>小数点キー</summary>
        Decimal = 110,
        /// <summary>除算記号 (/) キー</summary>
        Divide = 111,
        /// <summary>The F1 key.</summary>
        F1 = 112,
        /// <summary>The F2 key.</summary>
        F2 = 113,
        /// <summary>The F3 key.</summary>
        F3 = 114,
        /// <summary>The F4 key.</summary>
        F4 = 115,
        /// <summary>The F5 key.</summary>
        F5 = 116,
        /// <summary>The F6 key.</summary>
        F6 = 117,
        /// <summary>The F7 key.</summary>
        F7 = 118,
        /// <summary>The F8 key.</summary>
        F8 = 119,
        /// <summary>The F9 key.</summary>
        F9 = 120,
        /// <summary>The F10 key.</summary>
        F10 = 121,
        /// <summary>The F11 key.</summary>
        F11 = 122,
        /// <summary>The F12 key.</summary>
        F12 = 123,
        /// <summary>The F13 key.</summary>
        F13 = 124,
        /// <summary>The F14 key.</summary>
        F14 = 125,
        /// <summary>The F15 key.</summary>
        F15 = 126,
        /// <summary>The F16 key.</summary>
        F16 = 127,
        /// <summary>The F17 key.</summary>
        F17 = 128,
        /// <summary>The F18 key.</summary>
        F18 = 129,
        /// <summary>The F19 key.</summary>
        F19 = 130,
        /// <summary>The F20 key.</summary>
        F20 = 131,
        /// <summary>The F21 key.</summary>
        F21 = 132,
        /// <summary>The F22 key.</summary>
        F22 = 133,
        /// <summary>The F23 key.</summary>
        F23 = 134,
        /// <summary>The F24 key.</summary>
        F24 = 135,
        /// <summary>The NUM LOCK key.</summary>
        NumLock = 144,
        /// <summary>ScrollLock キー</summary>
        Scroll = 145,
        /// <summary>左の Shift キー</summary>
        LShiftKey = 160,
        /// <summary>右の Shift キー</summary>
        RShiftKey = 161,
        /// <summary>左の Ctrl キー</summary>
        LControlKey = 162,
        /// <summary>右の Ctrl キー</summary>
        RControlKey = 163,
        /// <summary>左の Alt キー</summary>
        LMenu = 164,
        /// <summary>右の Alt キー</summary>
        RMenu = 165,
        /// <summary>戻るキー (Windows 2000 以降)</summary>
        BrowserBack = 166,
        /// <summary>進むキー (Windows 2000 以降)</summary>
        BrowserForward = 167,
        /// <summary>更新キー (Windows 2000 以降)</summary>
        BrowserRefresh = 168,
        /// <summary>中止キー (Windows 2000 以降)</summary>
        BrowserStop = 169,
        /// <summary>検索キー (Windows 2000 以降)</summary>
        BrowserSearch = 170,
        /// <summary>お気に入りキー (Windows 2000 以降)</summary>
        BrowserFavorites = 171,
        /// <summary>ホーム キー (Windows 2000 以降)</summary>
        BrowserHome = 172,
        /// <summary>ミュート キー (Windows 2000 以降)</summary>
        VolumeMute = 173,
        /// <summary>音量 - キー (Windows 2000 以降)</summary>
        VolumeDown = 174,
        /// <summary>音量 + キー (Windows 2000 以降)</summary>
        VolumeUp = 175,
        /// <summary>次のトラック キー (Windows 2000 以降)</summary>
        MediaNextTrack = 176,
        /// <summary>前のトラック キー (Windows 2000 以降)</summary>
        MediaPreviousTrack = 177,
        /// <summary>停止キー (Windows 2000 以降)</summary>
        MediaStop = 178,
        /// <summary>再生/一時停止キー (Windows 2000 以降)</summary>
        MediaPlayPause = 179,
        /// <summary>メール ホット キー (Windows 2000 以降)</summary>
        LaunchMail = 180,
        /// <summary>メディア キー (Windows 2000 以降)</summary>
        SelectMedia = 181,
        /// <summary>カスタム ホット キー 1 (Windows 2000 以降)</summary>
        LaunchApplication1 = 182,
        /// <summary>カスタム ホット キー 2 (Windows 2000 以降)</summary>
        LaunchApplication2 = 183,
        /// <summary>米国標準キーボード上の OEM セミコロン キー (Windows 2000 以降)</summary>
        OemSemicolon = 186,
        /// <summary>The OEM 1 key.</summary>
        Oem1 = 186,
        /// <summary>国または地域別キーボード上の OEM プラス キー (Windows 2000 以降)</summary>
        Oemplus = 187,
        /// <summary>国または地域別キーボード上の OEM コンマ キー (Windows 2000 以降)</summary>
        Oemcomma = 188,
        /// <summary>国または地域別キーボード上の OEM マイナス キー (Windows 2000 以降)</summary>
        OemMinus = 189,
        /// <summary>国または地域別キーボード上の OEM ピリオド キー (Windows 2000 以降)</summary>
        OemPeriod = 190,
        /// <summary>米国標準キーボード上の OEM 疑問符キー (Windows 2000 以降)</summary>
        OemQuestion = 191,
        /// <summary>The OEM 2 key.</summary>
        Oem2 = 191,
        /// <summary>米国標準キーボード上の OEM ティルダ キー (Windows 2000 以降)</summary>
        Oemtilde = 192,
        /// <summary>The OEM 3 key.</summary>
        Oem3 = 192,
        /// <summary>米国標準キーボード上の OEM 左角かっこキー (Windows 2000 以降)</summary>
        OemOpenBrackets = 219,
        /// <summary>The OEM 4 key.</summary>
        Oem4 = 219,
        /// <summary>米国標準キーボード上の OEM Pipe キー (Windows 2000 以降)</summary>
        OemPipe = 220,
        /// <summary>The OEM 5 key.</summary>
        Oem5 = 220,
        /// <summary>米国標準キーボード上の OEM 右角かっこキー (Windows 2000 以降)</summary>
        OemCloseBrackets = 221,
        /// <summary>The OEM 6 key.</summary>
        Oem6 = 221,
        /// <summary>米国標準キーボード上の OEM 一重/二重引用符キー (Windows 2000 以降)</summary>
        OemQuotes = 222,
        /// <summary>The OEM 7 key.</summary>
        Oem7 = 222,
        /// <summary>The OEM 8 key.</summary>
        Oem8 = 223,
        /// <summary>RT 102 キーのキーボード上の OEM 山かっこキーまたは円記号キー (Windows 2000 以降)</summary>
        OemBackslash = 226,
        /// <summary>The OEM 102 key.</summary>
        Oem102 = 226,
        /// <summary>ProcessKey キー</summary>
        ProcessKey = 229,
        /// <summary>Unicode 文字がキーストロークであるかのように渡されます。Packet のキー値は、キーボード以外の入力手段に使用される 32 ビット仮想キー値の下位ワードです。</summary>
        Packet = 231,
        /// <summary>
        /// 
        /// </summary>
        Hiragana = 242,
        /// <summary>
        /// 
        /// </summary>
        Hankaku = 243,
        /// <summary>
        /// 
        /// </summary>
        Zenkaku = 244,
        /// <summary>The ATTN key.</summary>
        Attn = 246,
        /// <summary>Crsel キー</summary>
        Crsel = 247,
        /// <summary>Exsel キー</summary>
        Exsel = 248,
        /// <summary>EraseEof キー</summary>
        EraseEof = 249,
        /// <summary>The PLAY key.</summary>
        Play = 250,
        /// <summary>The ZOOM key.</summary>
        Zoom = 251,
        /// <summary>今後使用するために予約されている定数</summary>
        NoName = 252,
        /// <summary>PA1 キー</summary>
        Pa1 = 253,
        /// <summary>Clear キー</summary>
        OemClear = 254,
        /// <summary>
        /// XBOX ONE ガイドキー
        /// </summary>
        Guide = 255,
        /// <summary>Shift 修飾子キー</summary>
        Shift = 65536,
        /// <summary>Ctrl 修飾子キー</summary>
        Control = 131072,
        /// <summary>Alt 修飾子キー</summary>
        Alt = 262144
    }
}
