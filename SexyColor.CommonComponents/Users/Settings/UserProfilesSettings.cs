using SexyColor.BusinessComponents;
using System;

namespace SexyColor.CommonComponents
{
    [Serializable]
    public class UserProfilesSettings
    {
        private int[] integrityProportions = null;
        /// <summary>
        /// 项目分值
        /// </summary>
        public int[] IntegrityProportions
        {
            get
            {
                if (integrityProportions == null)
                {
                    integrityProportions = new int[7];
                    integrityProportions[(int)ProfileIntegrityItems.Headimg] = 20;
                    integrityProportions[(int)ProfileIntegrityItems.Birthday] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.NowArea] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.Age] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.Sex] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.SexualOrientation] = 20;
                    integrityProportions[(int)ProfileIntegrityItems.Sarriage] = 20;
                }
                return integrityProportions;
            }
            set { integrityProportions = value; }
        }

        private int minIntegrity = 50;
        /// <summary>
        /// 最少完成度
        /// </summary>
        public int MinIntegrity { get => minIntegrity; set => minIntegrity = value; }

        private int maxPersonTag = 3;
        /// <summary>
        /// 最多贴标签数
        /// </summary>
        public int MaxPersonTag { get => maxPersonTag; set => maxPersonTag = value; }

        

    }
}
