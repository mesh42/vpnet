namespace VpNet.Interfaces
{
    interface ICellCache<TVpObject, TVector3, in TCell>
        where TVector3 : class, IVector3, new()
        where TVpObject : class, IVpObject<TVector3>, new()
        where TCell : class, ICell,new()
    {
        /// <summary>
        /// Adds the cell range to be cached specified by 2 cell (start to end cell)
        /// </summary>
        /// <param name="start">The starting cell (in no particular order).</param>
        /// <param name="end">The ending cell (in no particular order).</param>
        /// <returns>
        /// The number of cells that are in this range
        /// </returns>
        int AddCellRange(TCell start, TCell end);

        /// <summary>
        /// Adds a cell to be cached.
        /// </summary>
        /// <param name="cell">The cell.</param>
        void AddCell(TCell cell);
    }
}
