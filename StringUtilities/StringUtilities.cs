using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StringUtilities
{
    public class Base64Encode : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(InputText.Get<string>(executionContext));
            StringValue.Set(executionContext, System.Convert.ToBase64String(textBytes));
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Encoded Text")]
        public OutArgument<string> StringValue { get; set; }
    }
    public class Base64Decode : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            var textBytes = System.Convert.FromBase64String(InputText.Get<string>(executionContext));
            StringValue.Set(executionContext, System.Text.Encoding.UTF8.GetString(textBytes));
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Decode Text")]
        public OutArgument<string> StringValue { get; set; }
    }
    public class Contains : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            // Use the tracing service 
            tracer.Trace(FieldText.Get<string>(executionContext));
            tracer.Trace((InputText.Get<string>(executionContext)));
            if (CaseSense.Get<bool>(executionContext))
            {
                ContainsValue.Set(executionContext, FieldText.Get<string>(executionContext).Contains(InputText.Get<string>(executionContext)));
            }
            else
            {
                ContainsValue.Set(executionContext, FieldText.Get<string>(executionContext).ToUpper().Contains(InputText.Get<string>(executionContext).ToUpper()));
            }
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [RequiredArgument]
        [Input("Input Field to check if it Contains Input Text")]
        public InArgument<string> FieldText { get; set; }

        [RequiredArgument]
        [Input("Make case sensitive")]
        public InArgument<bool> CaseSense { get; set; }

        [Output("Contains String")]
        public OutArgument<bool> ContainsValue { get; set; }
    }
    public class CreateEmptySpaces : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            // Use the tracing service 
            tracer.Trace(InputNum.Get<int>(executionContext).ToString());
            string emptyStrings = "";
            for (int i = 0; i < InputNum.Get<int>(executionContext); i++)
            {
                emptyStrings = " " + emptyStrings;
            }
            tracer.Trace(emptyStrings);
            EmptyString.Set(executionContext, emptyStrings);
        }
        [RequiredArgument]
        [Input("Number Of Spaces")]
        public InArgument<int> InputNum { get; set; }

        [Output("Empty String")]
        public OutArgument<string> EmptyString { get; set; }
    }
    public class DecodeHtml : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            StringWriter myWriter = new StringWriter();
            // Decode the encoded string.
            HttpUtility.HtmlDecode(InputText.Get<string>(executionContext), myWriter);
            string decodedString = myWriter.ToString();
            DecodedHTML.Set(executionContext, decodedString);
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Decode HTML")]
        public OutArgument<string> DecodedHTML { get; set; }
    }
    public class EncodeHtml : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            StringWriter myWriter = new StringWriter();
            string myDecodedString = myWriter.ToString();
            // Encode the string.
            string myEncodedString = HttpUtility.HtmlEncode(InputText.Get<string>(executionContext));
            EncodedHTML.Set(executionContext, myEncodedString);
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Encode HTML")]
        public OutArgument<string> EncodedHTML { get; set; }
    }
    public class EndsWith : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            StringEndsWith.Set(executionContext, InputText.Get<string>(executionContext).EndsWith(SubstringText.Get<string>(executionContext)).ToString());
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [RequiredArgument]
        [Input("Substring Text Ends With")]
        public InArgument<string> SubstringText { get; set; }

        [Output("Ends With")]
        public OutArgument<string> StringEndsWith { get; set; }
    }
    public class Join : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            JoinedString.Set(executionContext, String1.Get<string>(executionContext) + Joiner.Get<string>(executionContext) + String2.Get<string>(executionContext));
        }
        [RequiredArgument]
        [Input("String 1")]
        public InArgument<string> String1 { get; set; }
        [RequiredArgument]
        [Input("String 2")]
        public InArgument<string> String2 { get; set; }

        [Input("Joiner")]
        public InArgument<string> Joiner { get; set; }

        [Output("Joined String")]
        public OutArgument<string> JoinedString { get; set; }
    }
    public class Length : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            StringLength.Set(executionContext, InputString.Get<string>(executionContext).Length);
        }
        [RequiredArgument]
        [Input("String")]
        public InArgument<string> InputString { get; set; }

        [Output("String Length")]
        public OutArgument<int> StringLength { get; set; }
    }
    public class PadLeft : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            if (InputInt.Get<int>(executionContext) == 0)
            {
                PadLeftString.Set(executionContext, PadString.Get<string>(executionContext) + InputString.Get<string>(executionContext));
            }
            else
            {
                string newPadString = "";
                for (int i=0;i < InputInt.Get<int>(executionContext); i++)
                {
                    newPadString = PadString.Get<string>(executionContext) + newPadString;
                }
                PadLeftString.Set(executionContext, newPadString + InputString.Get<string>(executionContext));
            }
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputString { get; set; }

        [RequiredArgument]
        [Input("Pad String")]
        public InArgument<string> PadString { get; set; }

        [Input("Number of Padding")]
        public InArgument<int> InputInt { get; set; }

        [Output("PadLeft")]
        public OutArgument<string> PadLeftString { get; set; }
    }
    public class PadRight : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            if (InputInt.Get<int>(executionContext) == 0)
            {
                PadRightString.Set(executionContext, InputString.Get<string>(executionContext) + PadString.Get<string>(executionContext));
            }
            else
            {
                string newPadString = "";
                for (int i = 0; i < InputInt.Get<int>(executionContext); i++)
                {
                    newPadString = PadString.Get<string>(executionContext) + newPadString;
                }
                PadRightString.Set(executionContext, InputString.Get<string>(executionContext) + newPadString);
            }
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputString { get; set; }

        [RequiredArgument]
        [Input("Pad String")]
        public InArgument<string> PadString { get; set; }

        [Input("Number of Padding")]
        public InArgument<int> InputInt { get; set; }

        [Output("PadRight")]
        public OutArgument<string> PadRightString { get; set; }
    }
    public class RandomString : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            // GET INPUTTEXT string and convert to array of Characters.
            int stringlen = InputText.Get<int>(executionContext);
            // Creating object of random class
            Random rand = new Random();
            int randValue;
            string newString = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number.
                randValue = rand.Next(0, 26);

                // Generating random character by converting
                // the random number into character.
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string.
                newString = newString + letter;
            }
            StringValue.Set(executionContext, newString);
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<int> InputText { get; set; }

        [Output("Random Text")]
        public OutArgument<string> StringValue { get; set; }
    }
    public class Replace : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            Replaced.Set(executionContext, InputText.Get<string>(executionContext).Replace(StringToReplace.Get<string>(executionContext), ReplacedString.Get<string>(executionContext)));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [RequiredArgument]
        [Input("String to Replace")]
        public InArgument<string> StringToReplace { get; set; }

        [RequiredArgument]
        [Input("Replaced String")]
        public InArgument<string> ReplacedString { get; set; }

        [Output("Replaced")]
        public OutArgument<string> Replaced { get; set; }
    }
    public class ReplaceWithSpace : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            Replaced.Set(executionContext, InputText.Get<string>(executionContext).Replace(StringToReplace.Get<string>(executionContext), " "));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [RequiredArgument]
        [Input("String to Replace")]
        public InArgument<string> StringToReplace { get; set; }

        [Output("Replaced")]
        public OutArgument<string> Replaced { get; set; }
    }
    public class Reverse : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            // GET INPUTTEXT string and convert to array of Characters.
            char[] newArray = InputText.Get<string>(executionContext).ToCharArray();
            string newString = "";
            for (int i = newArray.Length - 1; i >= 0; i--)
            {
                newString = newString + newArray[i];
            }
            StringValue.Set(executionContext, newString);
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Reverse Text")]
        public OutArgument<string> StringValue { get; set; }
    }
    public class StartsWith : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            StringStartsWith.Set(executionContext, InputText.Get<string>(executionContext).StartsWith(SubstringText.Get<string>(executionContext)).ToString());
        }
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [RequiredArgument]
        [Input("Substring Text Starts With")]
        public InArgument<string> SubstringText { get; set; }

        [Output("Starts With")]
        public OutArgument<string> StringStartsWith { get; set; }
    }
    public class Substring : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            if (IntLength.Get<int>(executionContext) > InputText.Get<string>(executionContext).Length - StartPosition.Get<int>(executionContext))
            {
                OutputString.Set(executionContext, InputText.Get<string>(executionContext).Substring(StartPosition.Get<int>(executionContext), InputText.Get<string>(executionContext).Length - StartPosition.Get<int>(executionContext)).ToString());
            }
            else
            {
                OutputString.Set(executionContext, InputText.Get<string>(executionContext).Substring(StartPosition.Get<int>(executionContext), IntLength.Get<int>(executionContext)).ToString());
            }
        }
        [RequiredArgument]
        [Input("String to Parse")]
        public InArgument<string> InputText { get; set; }

        [RequiredArgument]
        [Input("Start Position")]
        public InArgument<int> StartPosition { get; set; }

        [Input("Length")]
        public InArgument<int> IntLength { get; set; }

        [Output("Substring")]
        public OutArgument<string> OutputString { get; set; }
    }
    public class ToLower : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            ToLowerText.Set(executionContext, InputText.Get<string>(executionContext).ToLower());
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("ToLower")]
        public OutArgument<string> ToLowerText { get; set; }
    }
    public class ToTitleCase : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            ToTitleText.Set(executionContext, myTI.ToTitleCase(InputText.Get<string>(executionContext)));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("ToTitleCase")]
        public OutArgument<string> ToTitleText{ get; set; }
    }
    public class ToUpper : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            ToUpperText.Set(executionContext, InputText.Get<string>(executionContext).ToUpper());
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("ToUpper")]
        public OutArgument<string> ToUpperText { get; set; }
    }
    public class Trim : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            TrimmedText.Set(executionContext, InputText.Get<string>(executionContext).Trim());
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("Trim")]
        public OutArgument<string> TrimmedText { get; set; }
    }
    public class RemoveExtraWhiteSpace : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            RemovedExtraWhiteSpaceText.Set(executionContext, Regex.Replace(InputText.Get<string>(executionContext), @"\s+", " "));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("Removed Whitespace")]
        public OutArgument<string> RemovedExtraWhiteSpaceText { get; set; }
    }
    public class RemoveHTML : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            RemovedHTMLText.Set(executionContext, Regex.Replace(Regex.Replace(InputText.Get<string>(executionContext), "<.*?>", string.Empty).Trim(), @"\s+", " "));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("Removed HTML")]
        public OutArgument<string> RemovedHTMLText { get; set; }
    }
    public class UrlEncode : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            UrlEncodedText.Set(executionContext, HttpUtility.UrlEncode(InputText.Get<string>(executionContext)));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("Encoded URL")]
        public OutArgument<string> UrlEncodedText { get; set; }
    }
    public class UrlDecode : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            UrlDecodedText.Set(executionContext, HttpUtility.UrlDecode(InputText.Get<string>(executionContext)));
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("Decoded URL")]
        public OutArgument<string> UrlDecodedText { get; set; }
    }
    public class WordCount : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            int a = 0, myWord = 1;
            string str = InputText.Get<string>(executionContext);
            while (a <= str.Length - 1)
            {
                if (str[a] == ' ' || str[a] == '\n' || str[a] == '\t')
                { myWord++; a++;}
                else
                { a++; }
            }
        WordCountText.Set(executionContext, myWord.ToString());
        WordCountInt.Set(executionContext, myWord);
        }
        [RequiredArgument]
        [Input("Field to Update")]
        public InArgument<string> InputText { get; set; }

        [Output("WordCount")]
        public OutArgument<string> WordCountText { get; set; }
        [Output("WordCount")]
        public OutArgument<int> WordCountInt { get; set; }
    }
}
