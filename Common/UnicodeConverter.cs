using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LIMS_DEMO.Common
{
    public class UnicodeConverter
    {
        public string ConvertHtmltoUnicode(string text)
        {
            string _inputString = text;
            string _response = string.Empty;
            CharacterSetInitialization.Initialize();
            StringBuilder _stringBuilder = new StringBuilder(_inputString, _inputString.Length * 2);
            foreach(var item in CharacterSetInitialization.CharacterSets)
            {
                if (item.HTMLNamedCode != null)
                {
                    _stringBuilder.Replace(item.HTMLNamedCode, "0x" + item.Unicode);    
                }
                
            }
            return _response;
        }

        public string HtmlTagConverter(string text)
        {
            string _inputString = text;
            string _response = string.Empty;
            //string _outputString = Regex.Replace(_inputString, "N.t", "NET");
            HtmlTags _htmlTags = new HtmlTags();

            foreach (var item in _htmlTags.TagList)
            {
                Regex rgx = new Regex(item.Tag);
                foreach (Match match in rgx.Matches(text))
                {
                   string _str = FindTagName(match, item);
                   StringBuilder builder = new StringBuilder(text);
                   builder.Replace(match.Value, _str);
                   text = builder.ToString();

                   text = text.Replace("<p>", "");
                   text = text.Replace("</p>", "");
                   text = text.Replace("\n", "");
                }    
            }

            _response = text;

            return _response;
        }

        public string FindTagName(Match match, HtmlTag item)
        {
            string _outputString = string.Empty;
            if (match.Value.ToString().Contains(item.TagName))
            {
                if (item.TagName == "sup")
                {
                    _outputString = match.Value.Replace("<sup>", "");
                    _outputString = _outputString.Replace("</sup>", "");
                }
                else if (item.TagName == "sub")
                {
                    _outputString = match.Value.Replace("<sub>", "");
                    _outputString = _outputString.Replace("</sub>", "");
                }

                _outputString = ReplaceCharacter(_outputString, item.TagName);
            }
            return _outputString;
        }

        public string ReplaceCharacter(string text, string tagName)
        {
            string _outputString = string.Empty;
            if(text.Length > 1)
            {

                if (tagName == "sub")
                {
                    foreach (char item in text.ToCharArray())
                    {
                        switch (text)
                        {
                            case "0":
                                _outputString = _outputString.Insert(0, "₀");
                                break;
                            case "1":
                                _outputString = _outputString.Insert(0, "₁");
                                break;
                            case "2":
                                _outputString = _outputString.Insert(0, "₂");
                                break;
                            case "3":
                                _outputString = _outputString.Insert(0, "₃");
                                break;
                            case "4":
                                _outputString = _outputString.Insert(0, "₄");
                                break;
                            case "5":
                                _outputString = _outputString.Insert(0, "₅");
                                break;
                            case "6":
                                _outputString = _outputString.Insert(0, "₆");
                                break;
                            case "7":
                                _outputString = _outputString.Insert(0, "₇");
                                break;
                            case "8":
                                _outputString = _outputString.Insert(0, "₈");
                                break;
                            case "9":
                                _outputString = _outputString.Insert(0, "₉");
                                break;

                            case "10":
                                _outputString = _outputString.Insert(0, "₁₀");
                                break;
                            case "2.5":
                                _outputString = _outputString.Insert(0, "₂.₅");
                                break;
                            case "0-":
                                _outputString = _outputString.Insert(0, "₀ˍ");
                                break;
                            case "1-":
                                _outputString = _outputString.Insert(0, "₁ˍ");
                                break;
                            case "2-":
                                _outputString = _outputString.Insert(0, "₂ˍ");
                                break;
                            case "3-":
                                _outputString = _outputString.Insert(0, "₃ˍ");
                                break;
                            case "4-":
                                _outputString = _outputString.Insert(0, "₄ˍ");
                                break;
                            case "5-":
                                _outputString = _outputString.Insert(0, "₅ˍ");
                                break;
                            case "6-":
                                _outputString = _outputString.Insert(0, "₆ˍ");
                                break;
                            case "7-":
                                _outputString = _outputString.Insert(0, "₇ˍ");
                                break;
                            case "8-":
                                _outputString = _outputString.Insert(0, "₈ˍ");
                                break;
                            case "9-":
                                _outputString = _outputString.Insert(0, "₉ˍ");
                                break;

                            case "-0":
                                _outputString = _outputString.Insert(0, "ˍ₀");
                                break;
                            case "-1":
                                _outputString = _outputString.Insert(0, "ˍ₁");
                                break;
                            case "-2":
                                _outputString = _outputString.Insert(0, "ˍ₂");
                                break;
                            case "-3":
                                _outputString = _outputString.Insert(0, "ˍ₃");
                                break;
                            case "-4":
                                _outputString = _outputString.Insert(0, "ˍ₄");
                                break;
                            case "-5":
                                _outputString = _outputString.Insert(0, "ˍ₅");
                                break;
                            case "-6":
                                _outputString = _outputString.Insert(0, "ˍ₆");
                                break;
                            case "-7":
                                _outputString = _outputString.Insert(0, "ˍ₇");
                                break;
                            case "-8":
                                _outputString = _outputString.Insert(0, "ˍ₈");
                                break;
                            case "-9":
                                _outputString = _outputString.Insert(0, "ˍ₉");
                                break;
                            case "-":
                                _outputString = _outputString.Insert(0, "ˍ");
                                break;

                            default:
                                _outputString = _outputString.Insert(0, text);
                                break;
                        }
                    }
                }
                else if (tagName == "sup")
                {
                    switch (text)
                    {
                        case "0":
                            _outputString = _outputString.Insert(0, "0");
                            break;
                        case "1":
                            _outputString = _outputString.Insert(0, "¹");
                            break;
                        case "2":
                            _outputString = _outputString.Insert(0, "²");
                            break;
                        case "3":
                            _outputString = _outputString.Insert(0, "³");
                            break;
                        case "4":
                            _outputString = _outputString.Insert(0, "⁴");
                            break;
                        case "5":
                            _outputString = _outputString.Insert(0, "⁵");
                            break;
                        case "6":
                            _outputString = _outputString.Insert(0, "⁶");
                            break;
                        case "7":
                            _outputString = _outputString.Insert(0, "⁷");
                            break;
                        case "8":
                            _outputString = _outputString.Insert(0, "⁸");
                            break;
                        case "9":
                            _outputString = _outputString.Insert(0, "⁹");
                            break;
                        case "o":
                            _outputString = _outputString.Insert(0, "°"); 
                            break;
                        case "O":
                            _outputString = _outputString.Insert(0, "°");
                            break;
                       
                        case "-":
                            _outputString = _outputString.Insert(0, "ˉ");
                            break;

                
                        case "1-":
                            _outputString = _outputString.Insert(0, "¹ˉ");
                            break;
                        case "2-":
                            _outputString = _outputString.Insert(0, "²ˉ");
                            break;
                        case "3-":
                            _outputString = _outputString.Insert(0, "³ˉ");
                            break;
                        case "4-":
                            _outputString = _outputString.Insert(0, "⁴ˉ");
                            break;
                        case "5-":
                            _outputString = _outputString.Insert(0, "⁵ˉ");
                            break;
                        case "6-":
                            _outputString = _outputString.Insert(0, "⁶ˉ");
                            break;
                        case "7-":
                            _outputString = _outputString.Insert(0, "⁷ˉ");
                            break;
                        case "8-":
                            _outputString = _outputString.Insert(0, "⁸ˉ");
                            break;
                        case "9-":
                            _outputString = _outputString.Insert(0, "⁹ˉ");
                            break;                  
                        case "O-":
                            _outputString = _outputString.Insert(0, "°ˉ");
                            break;

                        case "-1":
                            _outputString = _outputString.Insert(0, "ˉ¹");
                            break;
                        case "-2":
                            _outputString = _outputString.Insert(0, "ˉ²");
                            break;
                        case "-3":
                            _outputString = _outputString.Insert(0, "ˉ³");
                            break;
                        case "-4":
                            _outputString = _outputString.Insert(0, "ˉ⁴");
                            break;
                        case "-5":
                            _outputString = _outputString.Insert(0, "ˉ⁵");
                            break;
                        case "-6":
                            _outputString = _outputString.Insert(0, "ˉ⁶");
                            break;
                        case "-7":
                            _outputString = _outputString.Insert(0, "ˉ⁷");
                            break;
                        case "-8":
                            _outputString = _outputString.Insert(0, "ˉ⁸");
                            break;
                        case "-9":
                            _outputString = _outputString.Insert(0, "ˉ⁹");
                            break;
                        case "-O":
                            _outputString = _outputString.Insert(0, "ˉ°");
                            break;
                        case "rd":
                            _outputString = _outputString.Insert(0, "ʳᵈ");
                            break;
                        case "nd":
                            _outputString = _outputString.Insert(0, "ⁿᵈ");
                            break;
                        default:
                            _outputString = _outputString.Insert(0, text);
                            break;
                       
                    }
                }
            }
            else
            {
                if (tagName == "sub")
                {
                    switch (text)
                    {
                        case "0":
                            _outputString = _outputString.Insert(0, "₀");
                            break;
                        case "1":
                            _outputString = _outputString.Insert(0, "₁");
                            break;
                        case "2":
                            _outputString = _outputString.Insert(0, "₂");
                            break;
                        case "3":
                            _outputString = _outputString.Insert(0, "₃");
                            break;
                        case "4":
                            _outputString = _outputString.Insert(0, "₄");
                            break;
                        case "5":
                            _outputString = _outputString.Insert(0, "₅");
                            break;
                        case "6":
                            _outputString = _outputString.Insert(0, "₆");
                            break;
                        case "7":
                            _outputString = _outputString.Insert(0, "₇");
                            break;
                        case "8":
                            _outputString = _outputString.Insert(0, "₈");
                            break;
                        case "9":
                            _outputString = _outputString.Insert(0, "₉");
                            break;
                        case "-":
                            _outputString = _outputString.Insert(0, "ˍ");
                            break;
                        case "10":
                            _outputString = _outputString.Insert(0, "₁₀");
                            break;
                        case "2.5":
                            _outputString = _outputString.Insert(0, "₂.₅");
                            break;
                        case "0-":
                            _outputString = _outputString.Insert(0, "₀ˍ");
                            break;
                        case "1-":
                            _outputString = _outputString.Insert(0, "₁ˍ");
                            break;
                        case "2-":
                            _outputString = _outputString.Insert(0, "₂ˍ");
                            break;
                        case "3-":
                            _outputString = _outputString.Insert(0, "₃ˍ");
                            break;
                        case "4-":
                            _outputString = _outputString.Insert(0, "₄ˍ");
                            break;
                        case "5-":
                            _outputString = _outputString.Insert(0, "₅ˍ");
                            break;
                        case "6-":
                            _outputString = _outputString.Insert(0, "₆ˍ");
                            break;
                        case "7-":
                            _outputString = _outputString.Insert(0, "₇ˍ");
                            break;
                        case "8-":
                            _outputString = _outputString.Insert(0, "₈ˍ");
                            break;
                        case "9-":
                            _outputString = _outputString.Insert(0, "₉ˍ");
                            break;

                        case "-0":
                            _outputString = _outputString.Insert(0, "ˍ₀");
                            break;
                        case "-1":
                            _outputString = _outputString.Insert(0, "ˍ₁");
                            break;
                        case "-2":
                            _outputString = _outputString.Insert(0, "ˍ₂");
                            break;
                        case "-3":
                            _outputString = _outputString.Insert(0, "ˍ₃");
                            break;
                        case "-4":
                            _outputString = _outputString.Insert(0, "ˍ₄");
                            break;
                        case "-5":
                            _outputString = _outputString.Insert(0, "ˍ₅");
                            break;
                        case "-6":
                            _outputString = _outputString.Insert(0, "ˍ₆");
                            break;
                        case "-7":
                            _outputString = _outputString.Insert(0, "ˍ₇");
                            break;
                        case "-8":
                            _outputString = _outputString.Insert(0, "ˍ₈");
                            break;
                        case "-9":
                            _outputString = _outputString.Insert(0, "ˍ₉");
                            break;
                        default:
                            _outputString = _outputString.Insert(0, text);
                            break;
                   
                    }
                }
                else if (tagName == "sup")
                {
                    switch (text)
                    {
                        case "0":
                            _outputString = _outputString.Insert(0, "³");
                            break;
                        case "1":
                            _outputString = _outputString.Insert(0, "¹");
                            break;
                        case "2":
                            _outputString = _outputString.Insert(0, "²");
                            break;
                        case "3":
                            _outputString = _outputString.Insert(0, "³");
                            break;
                        case "4":
                            _outputString = _outputString.Insert(0, "⁴");
                            break;
                        case "5":
                            _outputString = _outputString.Insert(0, "⁵");
                            break;
                        case "6":
                            _outputString = _outputString.Insert(0, "⁶");
                            break;
                        case "7":
                            _outputString = _outputString.Insert(0, "⁷");
                            break;
                        case "8":
                            _outputString = _outputString.Insert(0, "⁸");
                            break;
                        case "9":
                            _outputString = _outputString.Insert(0, "⁹");
                            break;
                        case "o":
                            _outputString = _outputString.Insert(0, "°");
                            break;
                        case "O":
                            _outputString = _outputString.Insert(0, "°");
                            break;
                        case "-":
                            _outputString = _outputString.Insert(0, "ˉ");
                            break;

                        case "1-":
                            _outputString = _outputString.Insert(0, "¹ˉ");
                            break;
                        case "2-":
                            _outputString = _outputString.Insert(0, "²ˉ");
                            break;
                        case "3-":
                            _outputString = _outputString.Insert(0, "³ˉ");
                            break;
                        case "4-":
                            _outputString = _outputString.Insert(0, "⁴ˉ");
                            break;
                        case "5-":
                            _outputString = _outputString.Insert(0, "⁵ˉ");
                            break;
                        case "6-":
                            _outputString = _outputString.Insert(0, "⁶ˉ");
                            break;
                        case "7-":
                            _outputString = _outputString.Insert(0, "⁷ˉ");
                            break;
                        case "8-":
                            _outputString = _outputString.Insert(0, "⁸ˉ");
                            break;
                        case "9-":
                            _outputString = _outputString.Insert(0, "⁹ˉ");
                            break;
                        case "O-":
                            _outputString = _outputString.Insert(0, "°ˉ");
                            break;

                        case "-1":
                            _outputString = _outputString.Insert(0, "ˉ¹");
                            break;
                        case "-2":
                            _outputString = _outputString.Insert(0, "ˉ²");
                            break;
                        case "-3":
                            _outputString = _outputString.Insert(0, "ˉ³");
                            break;
                        case "-4":
                            _outputString = _outputString.Insert(0, "ˉ⁴");
                            break;
                        case "-5":
                            _outputString = _outputString.Insert(0, "ˉ⁵");
                            break;
                        case "-6":
                            _outputString = _outputString.Insert(0, "ˉ⁶");
                            break;
                        case "-7":
                            _outputString = _outputString.Insert(0, "ˉ⁷");
                            break;
                        case "-8":
                            _outputString = _outputString.Insert(0, "ˉ⁸");
                            break;
                        case "-9":
                            _outputString = _outputString.Insert(0, "ˉ⁹");
                            break;
                        case "rd":
                            _outputString = _outputString.Insert(0, "ʳᵈ");
                            break;
                        case "nd":
                            _outputString = _outputString.Insert(0, "ⁿᵈ");
                            break;
                        default:
                            _outputString = _outputString.Insert(0, text);
                            break;
                    }
                }
                
            }
            return _outputString;
        }

         

    }

    public interface IHtmlTag
    {
        string Tag { get; set; }
        string TagName { get; set; }
    }

    public class HtmlTag : IHtmlTag
    {
        public HtmlTag()
        {

        }

        public HtmlTag(string Tag, string TagName)
        {
            this.Tag = Tag;
            this.TagName = TagName;
        }

        public string Tag { get; set; }
        public string TagName { get; set; }
    }

    public class HtmlTags
    {
        public HtmlTags()
        {
            TagList = new List<HtmlTag>();
            Initialize();
        }

        void Initialize()
        {
            TagList.Add(new HtmlTag("<sup>.*?</sup>", "sup"));
            TagList.Add(new HtmlTag("<sub>.*?</sub>", "sub"));
        }

        public List<HtmlTag> TagList { get; set; }

    }


    public interface ICharacterSet
    {
        string Unicode { get; set; }
        //string EscapeSequenceUnicode { get; set; }
        string HTMLNumericCode { get; set; }
        string HTMLNamedCode { get; set; }
        string Symbol { get; set; }
        string Description { get; set; }
    }

    public class CharacterSet : ICharacterSet
    {
        public CharacterSet()
        {

        }

        public CharacterSet(string Unicode, string HTMLNumericCode, string HTMLNamedCode, string Symbol, string Description)
        {
            this.Unicode = Unicode;
            //this.EscapeSequenceUnicode = EscapeSequenceUnicode;
            this.HTMLNumericCode = HTMLNumericCode;
            this.HTMLNamedCode = HTMLNamedCode;
            this.Symbol = Symbol;
            this.Description = Description;
        }
        public string Unicode { get; set; }
        //public string EscapeSequenceUnicode { get; set; }
        public string HTMLNumericCode { get; set; }
        public string HTMLNamedCode { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
    }

    public static class CharacterSetInitialization
    {
        public static void Initialize()
        {
            CharacterSets = new List<CharacterSet>();
            CharacterSets.Add(new CharacterSet("2070", "&#8304;", null, "₀", "Subscripts number 0"));
            CharacterSets.Add(new CharacterSet("2071", "&#8305;", null, "₁", "Subscripts number 1"));
            CharacterSets.Add(new CharacterSet("2072", "&#8306;", null, "₁", "Subscripts number 2"));
            CharacterSets.Add(new CharacterSet("2073", "&#8307;", null, "₁", "Subscripts number 3"));
            CharacterSets.Add(new CharacterSet("2074", "&#8308;", null, "⁴", "Subscripts number 4"));
            CharacterSets.Add(new CharacterSet("2075", "&#8309;", null, "⁵", "Subscripts number 5"));
            CharacterSets.Add(new CharacterSet("2076", "&#8310;", null, "⁶", "Subscripts number 6"));
            CharacterSets.Add(new CharacterSet("2077", "&#8311;", null, "⁷", "Subscripts number 7"));
            CharacterSets.Add(new CharacterSet("2078", "&#8312;", null, "⁸", "Subscripts number 8"));
            CharacterSets.Add(new CharacterSet("2079", "&#8313;", null, "⁹", "Subscripts number 9"));


            CharacterSets.Add(new CharacterSet("2080", "&#8320;", null, "₀", "Superscripts number 0"));
            CharacterSets.Add(new CharacterSet("2081", "&#8321;", null, "₁", "Superscripts number 1"));
            CharacterSets.Add(new CharacterSet("2082", "&#8322;", null, "₂", "Superscripts number 2"));
            CharacterSets.Add(new CharacterSet("2083", "&#8323;", null, "₃", "Superscripts number 3"));
            CharacterSets.Add(new CharacterSet("2084", "&#8324;", null, "₄", "Superscripts number 4"));
            CharacterSets.Add(new CharacterSet("2085", "&#8325;", null, "₁", "Superscripts number 5"));
            CharacterSets.Add(new CharacterSet("2086", "&#8326;", null, "₁", "Superscripts number 6"));
            CharacterSets.Add(new CharacterSet("2087", "&#8327;", null, "₁", "Superscripts number 7"));
            CharacterSets.Add(new CharacterSet("2088", "&#8328;", null, "₁", "Superscripts number 8"));
            CharacterSets.Add(new CharacterSet("2089", "&#8329;", null, "₁", "Superscripts number 9"));
            CharacterSets.Add(new CharacterSet("208A", "&#8330;", null, "₊", "Superscripts number +"));
            CharacterSets.Add(new CharacterSet("208B", "&#8331;", null, "₋", "Superscripts number -"));
            CharacterSets.Add(new CharacterSet("208C", "&#8332;", null, "₌", "Superscripts number ="));
            CharacterSets.Add(new CharacterSet("208D", "&#8333;", null, "₍", "Superscripts number ("));
            CharacterSets.Add(new CharacterSet("208E", "&#8334;", null, "₎", "Superscripts number )"));
        }

        public static List<CharacterSet> CharacterSets { get; set; }

    }

}
