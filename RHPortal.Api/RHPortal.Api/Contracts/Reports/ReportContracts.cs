namespace RhPortal.Api.Contracts.Reports;

public sealed record ReportCatalogItemResponse(
    string Id,
    string Icon,
    string Title,
    string Desc,
    string Scope
);

public sealed record ReportCellResponse(
    string? Text,
    string? ClassName,
    string? Icon
);

public sealed record ReportDataResponse(
    IReadOnlyList<string> Labels,
    IReadOnlyList<int> Values,
    IReadOnlyList<string> Headers,
    IReadOnlyList<IReadOnlyList<ReportCellResponse>> Rows
);

public sealed record ReportVagaLookupResponse(
    Guid Id,
    string? Codigo,
    string Titulo
);
