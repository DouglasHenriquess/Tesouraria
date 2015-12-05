$(function () {
    registraEventosPessoa();
});
function onModalPessoas() {
    $("#btModalPessoas").click(function () {
        iniciaModalPessoas();
    });
}
function onPesquisaPessoas() {
    $("#btPesquisaPessoas").click(function () {
        pesquisaPessoas();
    });
}
function onVinculaTodasPessoas() {
    $("#btSelecionaPessoas").click(function () {
        vinculaTodasPessoas();
    });
}
function onVinculaPessoas() {
    $("#tablePessoasPesquisa .btn-info").click(function () {
        vinculaPessoas($(this));
    });
}
function onDesvinculaPessoas() {
    $("#tablePessoasPesquisa .btn-success").click(function () {
        desvinculaPessoas($(this));
    });
}

function registraEventosPessoa() {
    onModalPessoas();
    onPesquisaPessoas();
    onVinculaTodasPessoas();
    onVinculaPessoas();
    onDesvinculaPessoas();
}
function iniciaModalPessoas() {
    $("#txtPesquisaPessoas").val("");
    $("#tablePessoasPesquisa > thead").empty();
    $("#tablePessoasPesquisa > tbody").empty();
    verificaTodasPessoasVinculadas();
}
function pesquisaPessoas() {
    $.ajax({
        type: "GET",
        url: "/Lancamento/PesquisaPessoas/",
        data: { pesquisa: $("#txtPesquisaPessoas").val().toUpperCase() },
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success == true) {
                constroiTabelaPesquisaPessoas(retorno.Pessoas);
                verificaTodasPessoasVinculadas();
            }
            else {
                alert("Erro ao pesquisar pessoas.");
            }
        }
    });
}
function vinculaTodasPessoas() {
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
function vinculaPessoas(selecionado) {
    if (selecionado.hasClass("btn-info")) {
        $("#tablePessoasPesquisa .btn-success").unbind("click");
        selecionado.removeClass("btn-info");
        selecionado.addClass("btn-success");
        constroiTabelaVinculoPessoas(selecionado.closest("tr"));
        verificaTodasPessoasVinculadas();
        onDesvinculaPessoas();
    }
}
function desvinculaPessoas(selecionado) {
    if (selecionado.hasClass("btn-success")) {
        $("#tablePessoasPesquisa .btn-info").unbind("click");
        selecionado.removeClass("btn-success");
        selecionado.addClass("btn-info");
        removeLinhaTabelaVinculoPessoas(selecionado.closest("tr"));
        verificaTodasPessoasVinculadas();
        onVinculaPessoas();
    }
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