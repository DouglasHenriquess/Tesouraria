$(function () {
    registraEventos();
});
function onExibeCalendarioInicio() {
    $("#inputGroupInicio .input-group-addon").click(function () {
        exibeCalendarioInicio();
    });
}
function onExibeCalendarioFim() {
    $("#inputGroupFim .input-group-addon").click(function () {
        exibeCalendarioFim();
    });
}
function onVolta() {
    $("#btVolta").click(function () {
        window.location.href = "/";
    });
}
function onSalva() {
    $("#btSalva").click(function () {
        cadastraLancamentos();
    });
}

function registraEventos() {
    onExibeCalendarioInicio();
    onExibeCalendarioFim();
    onVolta();
    onSalva();
}
function exibeCalendarioInicio() {
    $(function () {
        $("#datePickerInicio").datepicker({
            format: "mm/yyyy",
            minViewMode: 1,
            language: "pt-BR",
            autoclose: true
        });
    });
    $("#datePickerInicio").datepicker("show");
}
function exibeCalendarioFim() {
    $(function () {
        $("#datePickerFim").datepicker({
            format: "mm/yyyy",
            minViewMode: 1,
            language: "pt-BR",
            autoclose: true
        });
    });
    $("#datePickerFim").datepicker("show");
}
function cadastraLancamentos() {
    $.ajax({
        type: "POST",
        url: "/Lancamento/Create/",
        data: JSON.stringify(preencheDados()),
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success) {
                alert("Lançamentos cadastrados com sucesso.");
            }
            else {
                alert("Erro ao cadastrar lançamentos.");
            }
            window.location.href = "/";
        }
    });
}
function preencheDados() {
    var taxas = new Array();
    var pessoas = new Array();
    $("#tableTaxasSelecionadas > tbody tr").each(function () {
        var taxa = {
            TaxaId: $(this).find("td:eq(0)").text(),
            Valor: $(this).find("td:eq(2)").text()
        };
        taxas.push(taxa);
    });
    $("#tablePessoasSelecionadas > tbody tr").each(function () {
        var pessoa = {
            PessoaId: $(this).find("td:eq(0)").text()
        };
        pessoas.push(pessoa);
    });
    return {
        dataInicio: "10/" + $("#datePickerInicio").val(),
        dataFim: "10/" + $("#datePickerFim").val(),
        taxas: taxas,
        pessoas: pessoas
    };
}