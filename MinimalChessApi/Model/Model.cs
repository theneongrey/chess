﻿namespace MinimalChessApi.Results;

public record GameReferenceModel(string GameId);
public record GameModel(IEnumerable<string> Cells, string State, bool IsItWhitesTurn, bool IsCheckPending);

