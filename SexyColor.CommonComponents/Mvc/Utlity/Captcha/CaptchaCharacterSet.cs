using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{
    public enum CaptchaCharacterSet
    {
        /// <summary>
        /// 小写字母
        /// </summary>
        LowercaseLetters = 1,
        /// <summary>
        /// 大写字母
        /// </summary>
        UppercaseLetters = 2,
        /// <summary>
        ///大小写混合
        /// </summary>
        Letters = LowercaseLetters | UppercaseLetters,
        /// <summary>
        /// 数字0-9
        /// </summary>
        Digits = 4,
        /// <summary>
        /// 数字字母混合 
        /// </summary>
        LettersAndDigits = Letters | Digits
    }
}
