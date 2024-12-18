﻿public struct textspan {
    public textspan(int start, int len) {
        this.start = start;
        this.len = len;
    }

    public int start { get; }
    public int len { get; }
    public int end => start + len;

    public static textspan frombounds(int start, int end) { 
        var len = end - start;
        return new(start, len);
    }
}