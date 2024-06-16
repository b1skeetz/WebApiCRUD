﻿namespace BasicCRUD.Domain.Result;

public class BaseResult<T>
{
    public bool IsSuccess => ErrorMessage == null;
    public string ErrorMessage { get; set; }
    public int ErrorCode { get; set; }
    public T Data { get; set; }
}