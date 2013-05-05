using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OnScreenKeyboardDisplayLibrary.Enums;
using System.Configuration;

namespace OnScreenKeyboardDisplayLibrary.Configuration.Reader
{
    public static class KeyboardLayoutReader
    {
        #region Fields

        private const string BaseLayout = "BaseLayout.xml";
        private const string LayoutSchema = "LayoutSchema.xsd";
        private const string LayoutDirectory = "Layouts";

        private static readonly string layoutName;

        private static Dictionary<Keys, Rectangle[]> _Innerlist = new Dictionary<Keys, Rectangle[]>();

        private static int lastX = 0;
        private static int lastY = 0;
        private static KeyboardButtonType lastButtonType = KeyboardButtonType.Square;

        #endregion

        #region cTor(s)

        static KeyboardLayoutReader()
        {
            layoutName = string.Format("{0}.xml",ConfigurationManager.AppSettings["KeyboardLayout"]);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Read the config file and store everyting in a dictionnary to
        /// simplify the usage
        /// </summary>
        /// <param name="path"></param>
        public static void ReadConfigFile(string path)
        {
            using (FileStream baselayoutStream = new FileStream(Path.Combine(path, BaseLayout), FileMode.Open))
            using (FileStream schemaStream = new FileStream(Path.Combine(path, LayoutSchema), FileMode.Open))
            using (FileStream layoutStream = new FileStream(Path.Combine(path, LayoutDirectory, layoutName), FileMode.Open))
            using (StreamReader schemaReader = new StreamReader(schemaStream))
            {
                XmlDocument baseLayoutDoc = new XmlDocument();
                XmlDocument layoutDoc = new XmlDocument();

                XmlSchema schema = XmlSchema.Read(schemaStream, null);
                XmlReaderSettings settings = new XmlReaderSettings();
                bool izOK = true;

                settings.Schemas.Add(schema);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += delegate(object sender, ValidationEventArgs e)
                {
                    if (e.Severity == XmlSeverityType.Error)
                    {
                        izOK = false;
                    }
                };

                XmlReader baseLayoutReader = XmlReader.Create(baselayoutStream, settings);
                XmlReader layoutReader = XmlReader.Create(layoutStream, settings);

                baseLayoutDoc.Load(baseLayoutReader);
                layoutDoc.Load(layoutReader);

                if (izOK)
                {
                    //Source

                    XmlNode baseRoot = baseLayoutDoc["Layout"];
                    string keyName;
                    KeyboardButtonType buttonType;
                    string positionType;
                    string axisRef = string.Empty; ;
                    int x = -1;
                    int y = -1;
                    foreach (XmlNode node in baseRoot)
                    {
                        if (node.Name == "Key")
                        {
                            keyName = node["Value"].InnerText;
                            buttonType = GetButtonType(node["ButtonType"].InnerText);
                            positionType = node["PositionType"].InnerText;
                            if (positionType == "Relative")
                            {
                                axisRef = node["PositionType"].Attributes[0].Value;
                            }
                            if (node["X"] != null)
                            {
                                int.TryParse(node["X"].InnerText, out x);
                            }
                            if (node["Y"] != null)
                            {
                                int.TryParse(node["Y"].InnerText, out y);
                            }

                            _Innerlist.Add(GetKey(keyName), new Rectangle[2] { GetRectangle(positionType, axisRef, buttonType, x, y), Rectangle.Empty });
                            lastButtonType = buttonType;
                        }
                    }

                    //Destination

                    baseRoot = layoutDoc["Layout"];
                    foreach (XmlNode node in baseRoot)
                    {
                        if (node.Name == "Key")
                        {
                            keyName = node["Value"].InnerText;
                            buttonType = GetButtonType(node["ButtonType"].InnerText);
                            positionType = node["PositionType"].InnerText;
                            if (positionType == "Relative")
                            {
                                axisRef = node["PositionType"].Attributes[0].Value;
                            }
                            if (node["X"] != null)
                            {
                                int.TryParse(node["X"].InnerText, out x);
                            }
                            if (node["Y"] != null)
                            {
                                int.TryParse(node["Y"].InnerText, out y);
                            }

                            _Innerlist[GetKey(keyName)][1] = GetRectangle(positionType, axisRef, buttonType, x, y);
                            lastButtonType = buttonType;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse string and return Keys type
        /// Yeah that's ugly to read
        /// </summary>
        /// <param name="keyName">String to parse</param>
        /// <returns>The key as a usable Key Type</returns>
        private static Keys GetKey(string keyName)
        {
            switch (keyName)
            {
                #region Esc -> F12

                case "Esc":
                    return Keys.Escape;
                case "F1":
                    return Keys.F1;
                case "F2":
                    return Keys.F2;
                case "F3":
                    return Keys.F3;
                case "F4":
                    return Keys.F4;
                case "F5":
                    return Keys.F5;
                case "F6":
                    return Keys.F6;
                case "F7":
                    return Keys.F7;
                case "F8":
                    return Keys.F8;
                case "F9":
                    return Keys.F9;
                case "F10":
                    return Keys.F10;
                case "F11":
                    return Keys.F11;
                case "F12":
                    return Keys.F12;

                #endregion

                #region Tilde -> Backspace

                case "Tilde":
                    return Keys.OemTilde;
                case "1":
                    return Keys.D1;
                case "2":
                    return Keys.D2;
                case "3":
                    return Keys.D3;
                case "4":
                    return Keys.D4;
                case "5":
                    return Keys.D5;
                case "6":
                    return Keys.D6;
                case "7":
                    return Keys.D7;
                case "8":
                    return Keys.D8;
                case "9":
                    return Keys.D9;
                case "0":
                    return Keys.D0;
                case "Minus":
                    return Keys.OemMinus;
                case "Plus":
                    return Keys.OemPlus;
                case "Backspace":
                    return Keys.Back;

                #endregion

                #region Tab -> Backslash

                case "Tab":
                    return Keys.Tab;
                case "Q":
                    return Keys.Q;
                case "W":
                    return Keys.W;
                case "E":
                    return Keys.E;
                case "R":
                    return Keys.R;
                case "T":
                    return Keys.T;
                case "Y":
                    return Keys.Y;
                case "U":
                    return Keys.U;
                case "I":
                    return Keys.I;
                case "O":
                    return Keys.O;
                case "P":
                    return Keys.P;
                case "OpenBracket":
                    return Keys.OemOpenBrackets;
                case "CloseBracket":
                    return Keys.OemCloseBrackets;
                case "Backslash":
                    return Keys.OemBackslash;

                #endregion

                #region Capslock -> Enter

                case "CapsLock":
                    return Keys.CapsLock;
                case "A":
                    return Keys.A;
                case "S":
                    return Keys.S;
                case "D":
                    return Keys.D;
                case "F":
                    return Keys.F;
                case "G":
                    return Keys.G;
                case "H":
                    return Keys.H;
                case "J":
                    return Keys.J;
                case "K":
                    return Keys.K;
                case "L":
                    return Keys.L;
                case "Semicolon":
                    return Keys.OemSemicolon;
                case "Quotes":
                    return Keys.OemQuotes;
                case "Enter":
                    return Keys.Enter;

                #endregion

                #region Leftshift -> RightShift

                case "LeftShift":
                    return Keys.LeftShift;
                case "Z":
                    return Keys.Z;
                case "X":
                    return Keys.X;
                case "C":
                    return Keys.C;
                case "V":
                    return Keys.V;
                case "B":
                    return Keys.B;
                case "N":
                    return Keys.N;
                case "M":
                    return Keys.M;
                case "Comma":
                    return Keys.OemComma;
                case "Period":
                    return Keys.OemPeriod;
                case "Question":
                    return Keys.OemQuestion;
                case "RightShift":
                    return Keys.RightShift;

                #endregion

                #region Leftcontrol -> Rightcontrol

                case "LeftControl":
                    return Keys.LeftControl;
                case "LeftWindows":
                    return Keys.LeftWindows;
                case "LeftAlt":
                    return Keys.LeftAlt;
                case "Spacebar":
                    return Keys.Space;
                case "RightAlt":
                    return Keys.RightAlt;
                case "RightWindows":
                    return Keys.RightWindows;
                case "Menu":
                    return Keys.Apps;
                case "RightControl":
                    return Keys.RightControl;

                #endregion

                #region Misc

                case "PrintScreen":
                    return Keys.PrintScreen;
                case "ScrollLock":
                    return Keys.Scroll;
                case "Pause":
                    return Keys.Pause;
                case "Insert":
                    return Keys.Insert;
                case "Home":
                    return Keys.Home;
                case "PageUp":
                    return Keys.PageUp;
                case "Delete":
                    return Keys.Delete;
                case "End":
                    return Keys.End;
                case "PageDown":
                    return Keys.PageDown;

                #endregion

                #region Arrows

                case "Up":
                    return Keys.Up;
                case "Left":
                    return Keys.Left;
                case "Down":
                    return Keys.Down;
                case "Right":
                    return Keys.Right;

                #endregion

                #region Numpad

                case "NumLock":
                    return Keys.NumLock;
                case "Divide":
                    return Keys.Divide;
                case "Multiply":
                    return Keys.Multiply;
                case "Substract":
                    return Keys.Subtract;
                case "Numpad7":
                    return Keys.NumPad7;
                case "Numpad8":
                    return Keys.NumPad8;
                case "Numpad9":
                    return Keys.NumPad9;
                case "Numpad4":
                    return Keys.NumPad4;
                case "Numpad5":
                    return Keys.NumPad5;
                case "Numpad6":
                    return Keys.NumPad6;
                case "Add":
                    return Keys.Add;
                case "Numpad1":
                    return Keys.NumPad1;
                case "Numpad2":
                    return Keys.NumPad2;
                case "Numpad3":
                    return Keys.NumPad3;
                case "Numpad0":
                    return Keys.NumPad0;
                case "Decimal":
                    return Keys.Decimal;
                // Special Key, these value are only used here.
                case "NumpadEnter":
                    return Keys.F24;
                case "NumLockLight":
                    return Keys.F23;
                case "CapsLockLight":
                    return Keys.F22;
                case "ScrollLockLight":
                    return Keys.F21;

                #endregion

                default:
                    throw new IndexOutOfRangeException(string.Format("Specified key \"{0}\" cannot be found", keyName));

            }
        }

        /// <summary>
        /// Parse string and return Keyboard button type
        /// </summary>
        /// <param name="buttonType">String to parse</param>
        /// <returns>Usable button type</returns>
        private static KeyboardButtonType GetButtonType(string buttonType)
        {
            switch (buttonType)
            {
                case "Square":
                    return KeyboardButtonType.Square;

                case "Rectangle24":
                    return KeyboardButtonType.Rectangle24;

                case "Rectangle30":
                    return KeyboardButtonType.Rectangle30;

                case "Rectangle36":
                    return KeyboardButtonType.Rectangle36;

                case "Rectangle43":
                    return KeyboardButtonType.Rectangle43;

                case "Rectangle48":
                    return KeyboardButtonType.Rectangle48;

                case "Rectangle123":
                    return KeyboardButtonType.Rectangle123;

                case "VerticalRectangle":
                    return KeyboardButtonType.VerticalRectangle;

                default:
                    throw new IndexOutOfRangeException(string.Format("The specified button type \"{0}\" is not managed"));
            }
        }

        /// <summary>
        /// Get a width depending of the specified button type
        /// </summary>
        /// <param name="buttonType">The button type</param>
        /// <returns>Width depending of the type</returns>
        private static int GetButtonWidth(KeyboardButtonType buttonType)
        {
            switch (buttonType)
            {
                case KeyboardButtonType.Rectangle123:
                    return 123;

                case KeyboardButtonType.Rectangle24:
                    return 24;

                case KeyboardButtonType.Rectangle30:
                    return 30;

                case KeyboardButtonType.Rectangle36:
                    return 36;

                case KeyboardButtonType.Rectangle43:
                    return 43;

                case KeyboardButtonType.Rectangle48:
                    return 48;

                case KeyboardButtonType.Square:
                case KeyboardButtonType.VerticalRectangle:
                default:
                    return 20;
            }
        }

        /// <summary>
        /// Get a height depending of the specified button type
        /// </summary>
        /// <param name="buttonType">The button type</param>
        /// <returns>Heoght depending of the type</returns>
        private static int GetButtonHeight(KeyboardButtonType buttonType)
        {
            if (buttonType == KeyboardButtonType.VerticalRectangle)
            {
                return 38;
            }
            else
            {
                return 19;
            }
        }

        /// <summary>
        /// Build a rectangle using specified parameters and return it
        /// </summary>
        /// <param name="positionType">The position type (e.g. relative or absolutute)</param>
        /// <param name="axisRef">The axis we used for reference when using relative position type</param>
        /// <param name="buttonType">The type of the button (Square, rectangle, etc...)</param>
        /// <param name="x">X positoin</param>
        /// <param name="y">Y position</param>
        /// <returns>A pretty rectangle ready to use</returns>
        private static Rectangle GetRectangle(string positionType, string axisRef, KeyboardButtonType buttonType, int x, int y)
        {
            Rectangle rect = Rectangle.Empty;

            rect.Width = GetButtonWidth(buttonType);
            rect.Height = GetButtonHeight(buttonType);

            switch (positionType)
            {
                case "Absolute":
                    rect.X = x;
                    rect.Y = y;
                    break;

                case "Relative":

                    if (axisRef == "X")
                    {
                        rect.X = lastX + GetButtonWidth(lastButtonType);
                        rect.Y = lastY;
                    }
                    else
                    {
                        rect.X = x;
                        rect.Y = lastY + GetButtonHeight(lastButtonType);
                    }
                    break;

                default:
                    throw new IndexOutOfRangeException(string.Format("The specified position type \"{0}\" is not managed"));
            }


            lastX = rect.X;
            lastY = rect.Y;

            return rect;
        }

        /// <summary>
        /// Just empty the variables
        /// Call after you dont need them anymore to avoid
        /// useless memory usage
        /// </summary>
        public static void Purge()
        {
            _Innerlist.Clear();
        }

        public static Keys[] GetAllKeys()
        {
            return _Innerlist.Keys.ToArray<Keys>();
        }

        /// <summary>
        /// Returns 2 rectangles for specified key
        /// 1st is the source
        /// 2nd is the destination
        /// </summary>
        /// <param name="key">Key wanted configuration</param>
        /// <returns>Both source and distination in a Rectangle[]</returns>
        public static Rectangle[] GetKeyConfig(Keys key)
        {
            return _Innerlist[key];
        }

        #endregion
    }
}
