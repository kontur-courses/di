namespace TagCloud.TextProcessing
{
    public enum PartOfSpeech
    {
        /// <summary>
        ///     прилагательное
        /// </summary>
        A,

        /// <summary>
        ///     наречие
        /// </summary>
        ADV,

        /// <summary>
        ///     местоименное наречие
        /// </summary>
        ADVPRO,

        /// <summary>
        ///     числительное-прилагательное
        /// </summary>
        ANUM,

        /// <summary>
        ///     местоимение-прилагательное
        /// </summary>
        APRO,

        /// <summary>
        ///     часть композита - сложного слова
        /// </summary>
        COM,

        /// <summary>
        ///     союз
        /// </summary>
        CONJ,

        /// <summary>
        ///     междометие
        /// </summary>
        INTJ,

        /// <summary>
        ///     числительное
        /// </summary>
        NUM,

        /// <summary>
        ///     частица
        /// </summary>
        PART,

        /// <summary>
        ///     предлог
        /// </summary>
        PR,

        /// <summary>
        ///     существительное
        /// </summary>
        S,

        /// <summary>
        ///     местоимение-существительное
        /// </summary>
        SPRO,

        /// <summary>
        ///     глагол
        /// </summary>
        V
    }
}