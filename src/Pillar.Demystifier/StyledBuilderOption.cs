using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Pillar.Demystifier;

public class StyledBuilderOption
{
    /// <summary>
    /// Global option.
    /// Use by default if there is no option passed.
    /// </summary>
    public static StyledBuilderOption GlobalOption { get; set; } = new(true);

    public static StyledBuilderOption NoColorOption { get; } = new(false);

    public bool EnableColor { get; }
    
    /// <summary>
    /// The style <see cref="Exception.Message"/>.
    /// </summary>
    public Style MessageStyle{
        get;
        set;
    }  = new();

    /// <summary>
    /// this is path.no file name.
    /// </summary>
    public Style SourcePathStyle{
        get;
        set;
    }  = new();

    /// <summary>
    /// this is file name.no path.
    /// </summary>
    public Style SourceFileStyle{
        get;
        set;
    }  = new();

    public Style LineNumberStyle{
        get;
        set;
    }  = new();

    /// <summary>
    /// If ture,will only print the file name of the source file.(no directory path)
    /// </summary>
    public bool ShortenSourceFilePath { get; set; } = false;

    public Style KeywordAsyncStyle
    {
        get;
        set;
    } = new();

    public Style KeywordNewStyle{
        get;
        set;
    }  = new();

    public Style KeywordStaticStyle{
        get;
        set;
    }  = new();

    public Style GenericArgumentStyle{
        get;
        set;
    }  = new();

    public Style MethodNameStyle{
        get;
        set;
    }  = new();

    /// <summary>
    /// e.g. the `Namespace1.Namespace2.Class1` for method `Namespace1.Namespace2.Class1.Method()`
    /// </summary>
    public Style DeclaringTypeOfMethodStyle{
        get;
        set;
    }  = new();

    /// <summary>
    /// Submethod or lambda defined in the method.
    /// </summary>
    public Style SubMethodOrLambdaStyle{
        get;
        set;
    }  = new();

    /// <summary>
    /// such as in,ref,out...
    /// </summary>
    public Style ParamPrefixStyle{
        get;
        set;
    }  = new();

    public Style KeywordDynamicStyle{
        get;
        set;
    }  = new();

    public Style ParamNameStyle{
        get;
        set;
    }  = new();

    public Style ParamTypeStyle{
        get;
        set;
    }  = new();

    public Style MethodReturnTypeStyle{
        get;
        set;
    }  = new();
    
    /// <summary>
    /// ---->
    /// </summary>
    public Style InnerExceptionOpenStyle{
        get;
        set;
    }  = new();
    
    /// <summary>
    /// --- End of inner exception stack trace ---
    /// </summary>
    public Style InnerExceptionEndStyle{
        get;
        set;
    }  = new();

    public StyledBuilderOption(bool enableColor = true)
    {
        EnableColor = enableColor;
        if (enableColor)
        {
            // default setting
            MessageStyle.ForeColor = Color.Red;
            MessageStyle.IsBold = true;
            MessageStyle.IsUnderline = true;
            LineNumberStyle.ForeColor = Color.Yellow;

            KeywordNewStyle.ForeColor = Color.Blue;
            KeywordStaticStyle.ForeColor = Color.Blue;
            KeywordDynamicStyle.ForeColor = Color.Blue;
            KeywordAsyncStyle.ForeColor = Color.Cornflowerblue;

            SubMethodOrLambdaStyle.ForeColor = Color.Mediumorchid1;

            MethodNameStyle.IsItalic = true;
            MethodNameStyle.IsBold = true;
            MethodNameStyle.IsUnderline = true;
            MethodNameStyle.ForeColor = Color.Aqua;
            ParamNameStyle.IsItalic = true;

            SourceFileStyle.ForeColor = Color.Yellow;

            ParamTypeStyle.ForeColor = Color.Darkkhaki;
            MethodReturnTypeStyle.ForeColor = Color.Darkkhaki;

            InnerExceptionOpenStyle.BackgroundColor = Color.Red;
            InnerExceptionEndStyle.BackgroundColor = Color.Red;
        }
    }
}
