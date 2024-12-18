﻿using System.Collections;
using System.Runtime.ConstrainedExecution;

internal sealed class diagbag : IEnumerable<diag> {
    readonly List<diag> _diags = new();

    public IEnumerator<diag> GetEnumerator() => _diags.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void AddRange(diagbag diags)
        => _diags.AddRange(diags._diags);

    void report(textspan span, string msg) {
        var diag = new diag(span, msg);
        _diags.Add(diag);
    }

    public void report_invalid_num(textspan span, string text, Type type) {
        var msg = $"the num '{text}' isnt a valid <{type}>";
        report(span, msg);
    }

    public void report_bad_char(int pos, char c) {
        var msg = $"unknown char in input: '{c}'";
        report(new textspan(pos, 1), msg);
    }

    public void report_unex_tok(textspan span, syntype acttype, syntype extype) {
        var msg = $"unexpected token: <{acttype}>, expected type <{extype}>";
        report(span, msg);
    }

    public void report_undef_uni_oper(textspan span, string opertext, Type cstype) {
        var msg = $"unary operator '{opertext}' is not defined for type <{cstype}>";
        report(span, msg);
    }

    public void report_undef_bin_oper(textspan span, string opertext, Type leftcstype, Type rightcstype) {
        var msg = $"binary operator '{opertext}' is not defined for types <{leftcstype}> and <{rightcstype}>";
        report(span, msg);
    }

    public void report_undef_name(textspan span, string name) {
        var msg = $"variable '{name}' doesent exist";
        report(span, msg);
    }
}