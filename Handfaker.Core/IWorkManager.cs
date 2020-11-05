using System.Collections;
using System.Collections.Generic;

namespace Handfaker.Core
{
    public interface IWorkManager
    {
        /// <summary>
        /// The file name of the word file scope processed by the work manager.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Arbitrarily updates the word file based on the replace method within the given set of font families.
        /// </summary>
        /// <param name="fontFamilies">The list of font families to randomize</param>
        /// <param name="replaceType">The replace type option to use.</param>
        void UpdateTextFonts(IEnumerable<string> fontFamilies, ReplaceType replaceType);
    }
}