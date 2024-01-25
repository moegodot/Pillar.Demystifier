using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Mingmoe.Demystifier;

public class StyledBuilderOption
{

    public Style MessageStyle = new Style();

    public Style SourceFilePathStyle = new Style();

    public Style LineNumberStyle = new Style();

    public bool shortenSourceFilePath = false;

    public Style KeywordAsyncStyle = new Style();

    public Style KeywordNewStyle = new Style();

    public Style KeywordStaticStyle = new Style();

    public Style GenericArgumentStyle = new Style();

    public Style MethodNameStyle = new Style();

    public Style DeclaringTypeOfMethodStyle = new Style();

    public Style SubMethodOrLambdaStyle = new Style();

    /// <summary>
    /// such as in,ref,out...
    /// </summary>
    public Style ParamPrefixStyle = new Style();

    public Style KeywordDynamicStyle = new Style();

    public Style ParamNameStyle = new Style();

    public Style ParamTypeStyle = new Style();

    public Style MethodReturnTypeStyle = new Style();

    public StyledBuilderOption()
    {
        // default setting
        MessageStyle.ForeColor = Color.Red;
        LineNumberStyle.ForeColor = Color.Yellow;

        KeywordNewStyle.ForeColor = Color.Blue;
        KeywordStaticStyle.ForeColor = Color.Blue;
        KeywordDynamicStyle.ForeColor = Color.Blue;
        KeywordAsyncStyle.ForeColor = Color.Cornflowerblue;

        SubMethodOrLambdaStyle.ForeColor = Color.Darkviolet;
        
        MethodNameStyle.isItalic = true;
        MethodNameStyle.isBold = true;
        MethodNameStyle.isUnderline = true;
        MethodNameStyle.ForeColor = Color.Aqua;
        ParamNameStyle.isItalic = true;

        ParamTypeStyle.ForeColor = Color.Darkkhaki;
        MethodReturnTypeStyle.ForeColor = Color.Darkkhaki;
    }
}
