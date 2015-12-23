$(function () {
    registraEventos();
    configuraControles();
});
function onModalPessoas() {
    $("#btModalPessoas").click(function () {
        iniciaModalPessoas();
    });
}
function onPesquisaPessoas() {
    $("#btPesquisa").click(function () {
        pesquisaPessoas();
    });
}
function onSelecionaTodasTaxas() {
    $("#btSelecionaTodasTaxas").click(function () {
        selecionaTodasTaxas();
    });
}
function onObtemLancamentosFuturos() {
    $("#btLancamentosFuturos").click(function () {
        obtemLancamentosFuturos();
    });
}

function registraEventos() {
    onModalPessoas();
    onPesquisaPessoas();
    onSelecionaTodasTaxas();
    onObtemLancamentosFuturos();
}
function configuraControles() {
    $("#btSelecionaTodasTaxas").hide();
    $("#btLancamentosFuturos").hide();
}
function iniciaModalPessoas() {
    $("#tablePesquisa > thead").empty();
    $("#tablePesquisa > tbody").empty();
    $("#tableLancamentos > thead").empty();
    $("#tableLancamentos > tbody").empty();
    $("#btSelecionaTodasTaxas").removeClass("btn-success");
    $("#btSelecionaTodasTaxas").addClass("btn-primary");
    $("#btSelecionaTodasTaxas").hide();
    $("#txtPesquisa").val("");
    $("#txtCodigo").val("");
    $("#txtNome").val("");
    $("#txtLugar").val("");
    $("#txtValor").val("");
}
function pesquisaPessoas() {
    $.ajax({
        type: "GET",
        url: "/Caixa/PesquisaPessoas/",
        data: { pesquisa: $("#txtPesquisa").val().toUpperCase() },
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success == true) {
                constroiTabelaPesquisa(retorno.Pessoas);
            }
            else {
                alert("Erro ao pesquisar pessoas.");
            }
        }
    });
}
function constroiTabelaPesquisa(pessoas) {
    $("#tablePesquisa > thead").empty();
    $("#tablePesquisa > tbody").empty();
    if (pessoas.length > 0) {
        var cabecalho = $("<tr>");
        cabecalho.append($("<th class='col-sm-2'>Código</th>"));
        cabecalho.append($("<th class='col-sm-4'>Nome</th>"));
        cabecalho.append($("<th class='col-sm-4'>Lugar</th>"));
        cabecalho.append($("<th class='col-sm-2'>Selecionar</th>"));
        cabecalho.append($("</tr>"));
        $("#tablePesquisa > thead").append(cabecalho);
        for (var i = 0; i < pessoas.length; i++) {
            var row = $("<tr>");
            row.append($("<td>" + pessoas[i].PessoaId + "</td>"));
            row.append($("<td>" + pessoas[i].Nome + "</td>"));
            row.append($("<td>" + pessoas[i].Lugar + "</td>"));
            row.append($(
                "<td align='center'>" +
                    "<button type='button' class='btn btn-info' data-dismiss='modal' onclick='selecionaPessoa($(this))'>" +
                        "<span class='fa fa-check'></span>" +
                    "</button>" +
                "</td>"));
            row.append($("</tr>"));
            $("#tablePesquisa > tbody").append(row);
        }
    }
}
function selecionaPessoa(selecionado) {
    var row = selecionado.closest("tr");
    row.find("td:eq(3) .btn-info").removeClass("btn-info");
    row.find("td:eq(3) .btn").addClass("btn-success");
    $("#txtCodigo").val(row.find("td:eq(0)").text());
    $("#txtNome").val(row.find("td:eq(1)").text());
    $("#txtLugar").val(row.find("td:eq(2)").text());
    obtemLancamentos($("#txtCodigo").val());
}
function obtemLancamentos(pessoaId) {
    $.ajax({
        type: "GET",
        url: "/Caixa/ObtemLancamentos/",
        data: { pessoaId: pessoaId },
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success == true) {
                constroiTabelaLancamentos(retorno.Lancamentos);
            }
            else {
                alert("Erro ao obter lançamentos.");
            }
        }
    });
}
function obtemLancamentosFuturos() {
    $.ajax({
        type: "GET/",
        url: "/Caixa/ObtemLancamentosFuturos/",
        data:  JSON.stringify({ pessoaId: parseInt($("#txtCodigo").val()) }),
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success == true) {
                constroiTabelaLancamentos(retorno.Lancamentos);
            }
            else {
                alert("Erro ao obter lançamentos.");
            }
        }
    });
}
function constroiTabelaLancamentos(lancamentos) {
    $("#tableLancamentos > thead").empty();
    $("#tableLancamentos > tbody").empty();
    if (lancamentos.length > 0) {
        $("#btSelecionaTodasTaxas").show();
        $("#btLancamentosFuturos").show();
        var cabecalho = $("<tr>");
        cabecalho.append("<th>Taxa</th>");
        cabecalho.append("<th>Vencimento</th>");
        cabecalho.append("<th>Valor</th>");
        cabecalho.append("<th>Pagar</th>");
        cabecalho.append("</tr>");
        $("#tableLancamentos > thead").append(cabecalho);
        for (var i = 0; i < lancamentos.length; i++) {
            var row = $("<tr>");
            row.append("<td>" + lancamentos[i].Taxa.Nome + "</td>");
            row.append("<td>" + retornaData(new Date(parseInt(lancamentos[i].DataVencimento.substr(6, 13)))) + "</td>");
            row.append("<td>" + lancamentos[i].Taxa.Valor + "</td>");
            row.append($(
                "<td align='center'>" +
                    "<button type='button' class='btn btn-info' onclick='calculaValorTotal($(this))'>" +
                        "<span class='fa fa-check'></span>" +
                    "</button>" +
                "</td>"));
            row.append("</tr");
            $("#tableLancamentos > tbody").append(row);
        }
    }
}
function retornaData(data) {
    var dia = data.getDate();
    var mes = data.getMonth() + 1;
    var ano = data.getFullYear();
    return dia + "/" + (mes < 10 ? "0" + mes : mes) + "/" + ano;
}
function calculaValorTotal(selecionado) {
    var valorTotal = 0;
    selecionado.closest("tr").find("td:eq(3) button").toggleClass("btn-info btn-success");
    $("#tableLancamentos .btn-success").each(function () {
        valorTotal += parseFloat($(this).closest("tr").find("td:eq(2)").text());
    });
    $("#txtValor").val(valorTotal.toFixed(2));
    if ($("#tableLancamentos .btn-info").length == 0) {
        $("#btSelecionaTodasTaxas").removeClass("btn-primary").addClass("btn-success");
    }
    else {
        $("#btSelecionaTodasTaxas").removeClass("btn-success").addClass("btn-primary");
    }
}
function selecionaTodasTaxas() {
    if ($("#btSelecionaTodasTaxas").hasClass("btn-primary")) {
        $("#tableLancamentos .btn-info").each(function () {
            $(this).click();
        });
    }
    else {
        $("#tableLancamentos .btn-success").each(function () {
            $(this).click();
        });
    }
}