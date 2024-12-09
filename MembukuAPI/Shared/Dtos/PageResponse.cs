﻿namespace MembukuAPI.Shared.Dtos;

public class PageResponse<T> {
    public IEnumerable<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages {
        get {
            return (int)Math.Ceiling((double)TotalRecords / PageSize);
        }
    }
}
