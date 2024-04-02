using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Mingmoe.Demystifier;

public class StyledBuilderOption
{
    /// <summary>
    /// Global option.
    /// Use by default if there is no option passed.
    /// </summary>
    public static StyledBuilderOption GlobalOption { get; set; } = new StyledBuilderOption();

    /// <summary>
    /// The style <see cref="Exception.Message"/>.
    /// </summary>
    public Style MessageStyle = new Style();

    /// <summary>
    /// this is path.no file name.
    /// </summary>
    public Style SourcePathStyle = new Style();

    /// <summary>
    /// this is file name.no path.
    /// </summary>
    public Style SourceFileStyle = new Style();

    public Style LineNumberStyle = new Style();

    /// <summary>
    /// If ture,will only print the file name of the source file.(no directory path)
    /// </summary>
    public bool shortenSourceFilePath = false;

    public Style KeywordAsyncStyle = new Style();

    public Style KeywordNewStyle = new Style();

    public Style KeywordStaticStyle = new Style();

    public Style GenericArgumentStyle = new Style();

    public Style MethodNameStyle = new Style();

    /// <summary>
    /// e.g. the `Namespace1.Namespace2.Class1` for method `Namespace1.Namespace2.Class1.Method()`
    /// </summary>
    public Style DeclaringTypeOfMethodStyle = new Style();

    /// <summary>
    /// Submethod or lambda defined in the method.
    /// </summary>
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

        SubMethodOrLambdaStyle.ForeColor = Color.Mediumorchid1;

        MethodNameStyle.isItalic = true;
        MethodNameStyle.isBold = true;
        MethodNameStyle.isUnderline = true;
        MethodNameStyle.ForeColor = Color.Aqua;
        ParamNameStyle.isItalic = true;

        SourceFileStyle.ForeColor = Color.Yellow;

        ParamTypeStyle.ForeColor = Color.Darkkhaki;
        MethodReturnTypeStyle.ForeColor = Color.Darkkhaki;
    }
}
