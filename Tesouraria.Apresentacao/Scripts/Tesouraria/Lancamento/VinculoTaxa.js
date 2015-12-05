$(function () {
    registraEventosTaxa();
});
function onModalTaxas() {
    $("#btModalTaxas").click(function () {
        iniciaModalTaxas();
    });
}
function onPesquisaTaxas() {
    $("#btPesquisaTaxas").click(function () {
        pesquisaTaxas();
    });
}
function onVinculaTodasTaxas() {
    $("#btSelecionaTaxas").click(function () {
        vinculaTodasTaxas();
    });
}
function onVinculaTaxas() {
    $("#tableTaxasPesquisa .btn-info").click(function () {
        vinculaTaxas($(this));
    });
}
function onDesvinculaTaxas() {
    $("#tableTaxasPesquisa .btn-success").click(function () {
        desvinculaTaxas($(this));
    });
}

function registraEventosTaxa() {
    onModalTaxas();
    onPesquisaTaxas();
    onVinculaTodasTaxas();
    onVinculaTaxas();
    onDesvinculaTaxas();
}
function iniciaModalTaxas() {
    $("#txtPesquisaTaxas").val("");
    $("#tableTaxasPesquisa > thead").empty();
    $("#tableTaxasPesquisa > tbody").empty();
    verificaTodasTaxasVinculadas();
}
function pesquisaTaxas() {
    $.ajax({
        type: "GET",
        url: "/Lancamento/PesquisaTaxas/",
        data: { pesquisa: $("#txtPesquisaTaxas").val().toUpperCase() },
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success == true) {
                constroiTabelaPesquisaTaxas(retorno.Taxas);
                verificaTodasTaxasVinculadas();
            }
            else {
                alert("Erro ao pesquisar Taxas.");
            }
        }
    });
}
function vinculaTodasTaxas() {
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
    row.append($("<td>" + linha.Valor.toString().replace(".", ",") + "</td>"));
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
function vinculaTaxas(selecionado) {
    if (selecionado.hasClass("btn-info")) {
        $("#tableTaxasPesquisa .btn-success").unbind("click");
        selecionado.removeClass("btn-info");
        selecionado.addClass("btn-success");
        constroiTabelaVinculoTaxas(selecionado.closest("tr"));
        verificaTodasTaxasVinculadas();
        onDesvinculaTaxas();
    }
}
function desvinculaTaxas(selecionado) {
    if (selecionado.hasClass("btn-success")) {
        $("#tableTaxasPesquisa .btn-info").unbind("click");
        selecionado.removeClass("btn-success");
        selecionado.addClass("btn-info");
        removeLinhaTabelaVinculoTaxas(selecionado.closest("tr"));
        verificaTodasTaxasVinculadas();
        onVinculaTaxas();
    }
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