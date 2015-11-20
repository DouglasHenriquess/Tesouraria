$(function () {
    registraEventos();
});
function registraEventos() {
    onModalPessoas();
    onPesquisaPessoas();
    onVinculaTodasPessoas();
    onModalTaxas();
    onPesquisaTaxas();
    onVinculaTodasTaxas();
    onSelecionaDataInicio();
    onConfiguraCalendario();
    
}

//VÍNCULO DE PESSOAS
function onModalPessoas() {
    $("#btModalPessoas").click(function () {
        $("#txtPesquisaPessoas").val("");
        $("#tablePessoasPesquisa > thead").empty();
        $("#tablePessoasPesquisa > tbody").empty();
        verificaTodasPessoasVinculadas();
    });
}
function onPesquisaPessoas() {
    $("#btPesquisaPessoas").click(function () {
        $.ajax({
            type: "POST",
            url: "/Lancamento/PesquisaPessoas/",
            data: { pesquisa: $("#txtPesquisaPessoas").val().toUpperCase() },
            cache: false,
            success: function (retorno) {
                constroiTabelaPesquisaPessoas(retorno);
                verificaTodasPessoasVinculadas();
            }
        });
    });
}
function onVinculaTodasPessoas() {
    $("#btSelecionaPessoas").click(function () {
        if ($("#btSelecionaPessoas").hasClass("btn-primary")) {
            $("#tablePessoasPesquisa .btn-info").each(function () {
                $(this).click();
            });
        }
        else {
            $("#tablePessoasPesquisa .btn-success").each(function () {
                $(this).click();
            });
        }
    });
}
function constroiTabelaPesquisaPessoas(data) {
    $("#tablePessoasPesquisa > thead").empty();
    $("#tablePessoasPesquisa > tbody").empty();
    if (data.length > 0) {
        constroiCabecalhoTabelaPesquisaPessoas();
        for (var i = 0; i < data.length; i++) {
            constroiLinhaTabelaPesquisaPessoas(data[i]);
        }
    }
}
function constroiCabecalhoTabelaPesquisaPessoas() {
    var row = $("<tr>");
    row.append($("<th class='col-sm-2'>Código</th>"));
    row.append($("<th class='col-sm-4'>Nome</th>"));
    row.append($("<th class='col-sm-4'>Lugar</th>"));
    row.append($("<th class='col-sm-2'>Selecionar</th>"));
    row.append($("</tr>"))
    $("#tablePessoasPesquisa > thead").append(row);
}
function constroiLinhaTabelaPesquisaPessoas(linha) {
    var row = $("<tr>");
    row.append($("<td>" + linha.PessoaId + "</td>"))
    row.append($("<td>" + linha.Nome + "</td>"));
    row.append($("<td>" + linha.Lugar + "</td>"));
    if (verificaSelecaoTabelaVinculoPessoas(linha.PessoaId)) {
        row.append($(
            "<td align='center'>" +
                "<button type='button' class='btn btn-info'>" +
                    "<span class='fa fa-check'></span>" +
                "</button>" +
            "</td>"));
    }
    else {
        row.append($(
            "<td align='center'>" +
                "<button type='button' class='btn btn-success'>" +
                    "<span class='fa fa-check'></span>" +
                "</button>" +
            "</td>"));
    }
    row.append($("</tr>"));
    $("#tablePessoasPesquisa > tbody").append(row);
    onVinculaPessoas();
    onDesvinculaPessoas();
}
function verificaTodasPessoasVinculadas() {
    if ($("#tablePessoasPesquisa > tbody > tr").length > 0) {
        if ($("#tablePessoasPesquisa .btn-info").length == 0) {
            $("#btSelecionaPessoas").removeClass("btn-primary");
            $("#btSelecionaPessoas").addClass("btn-success");
        }
        else {
            $("#btSelecionaPessoas").removeClass("btn-success");
            $("#btSelecionaPessoas").addClass("btn-primary");
        }
    }
    else {
        $("#btSelecionaPessoas").removeClass("btn-success");
        $("#btSelecionaPessoas").addClass("btn-primary");
    }
}
function onVinculaPessoas() {
    $("#tablePessoasPesquisa .btn-info").click(function () {
        if ($(this).hasClass("btn-info")) {
            $("#tablePessoasPesquisa .btn-success").unbind("click");
            $(this).removeClass("btn-info");
            $(this).addClass("btn-success");
            constroiTabelaVinculoPessoas($(this).closest("tr"));
            verificaTodasPessoasVinculadas();
            onDesvinculaPessoas();
        }
    });
}
function onDesvinculaPessoas() {
    $("#tablePessoasPesquisa .btn-success").click(function () {
        if ($(this).hasClass("btn-success")) {
            $("#tablePessoasPesquisa .btn-info").unbind("click");
            $(this).removeClass("btn-success");
            $(this).addClass("btn-info");
            removeLinhaTabelaVinculoPessoas($(this).closest("tr"));
            verificaTodasPessoasVinculadas();
            onVinculaPessoas();
        }
    });
}
function constroiTabelaVinculoPessoas(linha) {
    constroiCabecalhoTabelaVinculoPessoas();
    constroiLinhaTabelaVinculoPessoas(linha);
}
function constroiCabecalhoTabelaVinculoPessoas() {
    if ($("#tablePessoasSelecionadas > thead > tr").length == 0) {
        var row = $("<tr>");
        row.append($("<th class='col-sm-2'>Código</th>"));
        row.append($("<th class='col-sm-5'>Nome</th>"));
        row.append($("<th class='col-sm-5'>Lugar</th>"));
        row.append($("</tr>"));
        $("#tablePessoasSelecionadas > thead").append(row);
    }
}
function constroiLinhaTabelaVinculoPessoas(linha) {
    if (verificaSelecaoTabelaVinculoPessoas(linha.find("td").eq(0).html())) {
        var row = $("<tr>");
        row.append($("<td>" + linha.find("td").eq(0).html() + "</td>"));
        row.append($("<td>" + linha.find("td").eq(1).html() + "</td>"));
        row.append($("<td>" + linha.find("td").eq(2).html() + "</td>"));
        row.append($("</tr>"));
        $("#tablePessoasSelecionadas > tbody").append(row);
    }
}
function removeLinhaTabelaVinculoPessoas(linha) {
    $("#tablePessoasSelecionadas tr > td:nth-child(1)").filter(function () {
        if ($(this).text() == linha.find("td").eq(0).html()) {
            $(this).parent().remove();
        }
    });
    if ($("#tablePessoasSelecionadas > tbody > tr").length == 0) {
        $("#tablePessoasSelecionadas > thead > tr").remove();
    }
}
function verificaSelecaoTabelaVinculoPessoas(pessoaId) {
    var retorno = true;
    $("#tablePessoasSelecionadas tr > td:nth-child(1)").filter(function () {
        if ($(this).text() == pessoaId) {
            retorno = false;
        }
    });
    return retorno;
}

//VÍNCULO DE TAXAS
function onModalTaxas() {
    $("#btModalTaxas").click(function () {
        $("#txtPesquisaTaxas").val("");
        $("#tableTaxasPesquisa > thead").empty();
        $("#tableTaxasPesquisa > tbody").empty();
        verificaTodasTaxasVinculadas();
    });
}
function onPesquisaTaxas() {
    $("#btPesquisaTaxas").click(function () {
        $.ajax({
            type: "POST",
            url: "/Lancamento/PesquisaTaxas/",
            data: { pesquisa: $("#txtPesquisaTaxas").val().toUpperCase() },
            cache: false,
            success: function (retorno) {
                constroiTabelaPesquisaTaxas(retorno);
                verificaTodasTaxasVinculadas();
            }
        });
    });
}
function onVinculaTodasTaxas() {
    $("#btSelecionaTaxas").click(function () {
        if ($("#btSelecionaTaxas").hasClass("btn-primary")) {
            $("#tableTaxasPesquisa .btn-info").each(function () {
                $(this).click();
            });
        }
        else {
            $("#tableTaxasPesquisa .btn-success").each(function () {
                $(this).click();
            });
        }
    });
}
function constroiTabelaPesquisaTaxas(data) {
    $("#tableTaxasPesquisa > thead").empty();
    $("#tableTaxasPesquisa > tbody").empty();
    if (data.length > 0) {
        constroiCabecalhoTabelaPesquisaTaxas();
        for (var i = 0; i < data.length; i++) {
            constroiLinhaTabelaPesquisaTaxas(data[i]);
        }
    }
}
function constroiCabecalhoTabelaPesquisaTaxas() {
    var row = $("<tr>");
    row.append($("<th class='col-sm-2'>Código</th>"));
    row.append($("<th class='col-sm-6'>Nome</th>"));
    row.append($("<th class='col-sm-2'>Valor</th>"));
    row.append($("<th class='col-sm-2'>Selecionar</th>"));
    row.append($("</tr>"))
    $("#tableTaxasPesquisa > thead").append(row);
}
function constroiLinhaTabelaPesquisaTaxas(linha) {
    var row = $("<tr>");
    row.append($("<td>" + linha.TaxaId + "</td>"))
    row.append($("<td>" + linha.Nome + "</td>"));
    row.append($("<td>" + linha.Valor + "</td>"));
    if (verificaSelecaoTabelaVinculoTaxas(linha.TaxaId)) {
        row.append($(
            "<td align='center'>" +
                "<button type='button' class='btn btn-info'>" +
                    "<span class='fa fa-check'></span>" +
                "</button>" +
            "</td>"));
    }
    else {
        row.append($(
            "<td align='center'>" +
                "<button type='button' class='btn btn-success'>" +
                    "<span class='fa fa-check'></span>" +
                "</button>" +
            "</td>"));
    }
    row.append($("</tr>"));
    $("#tableTaxasPesquisa > tbody").append(row);
    onVinculaTaxas();
    onDesvinculaTaxas();
}
function verificaTodasTaxasVinculadas() {
    if ($("#tableTaxasPesquisa > tbody > tr").length > 0) {
        if ($("#tableTaxasPesquisa .btn-info").length == 0) {
            $("#btSelecionaTaxas").removeClass("btn-primary");
            $("#btSelecionaTaxas").addClass("btn-success");
        }
        else {
            $("#btSelecionaTaxas").removeClass("btn-success");
            $("#btSelecionaTaxas").addClass("btn-primary");
        }
    }
    else {
        $("#btSelecionaTaxas").removeClass("btn-success");
        $("#btSelecionaTaxas").addClass("btn-primary");
    }
}
function onVinculaTaxas() {
    $("#tableTaxasPesquisa .btn-info").click(function () {
        if ($(this).hasClass("btn-info")) {
            $("#tableTaxasPesquisa .btn-success").unbind("click");
            $(this).removeClass("btn-info");
            $(this).addClass("btn-success");
            constroiTabelaVinculoTaxas($(this).closest("tr"));
            verificaTodasTaxasVinculadas();
            onDesvinculaTaxas();
        }
    });
}
function onDesvinculaTaxas() {
    $("#tableTaxasPesquisa .btn-success").click(function () {
        if ($(this).hasClass("btn-success")) {
            $("#tableTaxasPesquisa .btn-info").unbind("click");
            $(this).removeClass("btn-success");
            $(this).addClass("btn-info");
            removeLinhaTabelaVinculoTaxas($(this).closest("tr"));
            verificaTodasTaxasVinculadas();
            onVinculaTaxas();
        }
    });
}
function constroiTabelaVinculoTaxas(linha) {
    constroiCabecalhoTabelaVinculoTaxas();
    constroiLinhaTabelaVinculoTaxas(linha);
}
function constroiCabecalhoTabelaVinculoTaxas() {
    if ($("#tableTaxasSelecionadas > thead > tr").length == 0) {
        var row = $("<tr>");
        row.append($("<th class='col-sm-2'>Código</th>"));
        row.append($("<th class='col-sm-8'>Nome</th>"));
        row.append($("<th class='col-sm-2'>Valor</th>"));
        row.append($("</tr>"));
        $("#tableTaxasSelecionadas > thead").append(row);
    }
}
function constroiLinhaTabelaVinculoTaxas(linha) {
    if (verificaSelecaoTabelaVinculoTaxas(linha.find("td").eq(0).html())) {
        var row = $("<tr>");
        row.append($("<td>" + linha.find("td").eq(0).html() + "</td>"));
        row.append($("<td>" + linha.find("td").eq(1).html() + "</td>"));
        row.append($("<td>" + linha.find("td").eq(2).html() + "</td>"));
        row.append($("</tr>"));
        $("#tableTaxasSelecionadas > tbody").append(row);
    }
}
function removeLinhaTabelaVinculoTaxas(linha) {
    $("#tableTaxasSelecionadas tr > td:nth-child(1)").filter(function () {
        if ($(this).text() == linha.find("td").eq(0).html()) {
            $(this).parent().remove();
        }
    });
    if ($("#tableTaxasSelecionadas > tbody > tr").length == 0) {
        $("#tableTaxasSelecionadas > thead > tr").remove();
    }
}
function verificaSelecaoTabelaVinculoTaxas(pessoaId) {
    var retorno = true;
    $("#tableTaxasSelecionadas tr > td:nth-child(1)").filter(function () {
        if ($(this).text() == pessoaId) {
            retorno = false;
        }
    });
    return retorno;
}

//CALENDÁRIOS
function onSelecionaDataInicio() {
    $("#datePickerInicio").click(function () {
        $("#datePickerInicio").datepicker("show");
//        $(".input-group .date").datepicker({
//            format: "dd/mm/yyyy",
//            language: "pt-BR",
//            orientation: "top auto",
//            autoclose: true
//        });
    });
}

//function onConfiguraCalendarios() {
//   
//}