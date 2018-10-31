namespace SexyColor.Infrastructure
{
    public enum ResizeMethod
    {
        /// <summary>
        /// 按绝对尺寸缩放 (按指定的尺寸进行缩放,不保证宽高比率，可能导致图像失真)
        /// </summary>
        Absolute = 0,
 
        /// <summary>
        /// 保持原图像宽高比缩放 (保持原图像宽高比进行缩放，不超出指定宽高构成的矩形范围)
        /// </summary>
        KeepAspectRatio = 1,
 
        /// <summary>
        /// 裁剪图像 (保持原图像宽高比)
        /// </summary>
        Crop = 3
    }
}
