﻿using System;
using static H3.Constants;



namespace H3.Model {

public sealed class BaseCellRotation {
    public int Cell;
    public int CounterClockwiseRotations;
    public BaseCell BaseCell;

    public const int InvalidRotations = -1;

    private BaseCellRotation() { }

    public static implicit operator BaseCellRotation((int, int) tuple) =>
        new() {
            Cell = tuple.Item1,
            CounterClockwiseRotations = tuple.Item2,
            BaseCell =  BaseCells.Cells[tuple.Item1]
        };

    public static int GetCounterClockwiseRotationsForBaseCell(int cell, int face) {
        if (face is < 0 or > NUM_ICOSA_FACES) return InvalidRotations;

        for (var i = 0; i < 3; i+= 1) {
            for (var j = 0; j < 3; j += 1) {
                for (var k = 0; k < 3; k += 1) {
                    var e = LookupTables.FaceIjkBaseCells[face, i, j, k];
                    if (e.Cell == cell) return e.CounterClockwiseRotations;
                }
            }
        }

        return InvalidRotations;
    }

    public override bool Equals(object other) => other is BaseCellRotation r &&
                                                  Cell == r.Cell &&
                                                  CounterClockwiseRotations == r.CounterClockwiseRotations;

    public override int GetHashCode() => HashCode.Combine(Cell, CounterClockwiseRotations);
}

}