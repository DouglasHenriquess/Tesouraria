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
function onNovo() {
    $("#btNovo").click(function () {
        window.location.href = "/Caixa/Create";
    });
}
function onPesquisa() {
    $("#btPesquisa").click(function () {
        pesquisaRecebimentos();
    });
}

function registraEventos() {
    onExibeCalendarioInicio();
    onExibeCalendarioFim();
    onNovo();
    onPesquisa();
}
function exibeCalendarioInicio() {
    $(function () {
        $("#datePickerInicio").datepicker({
            language: "pt-BR",
            autoclose: true
        });
    });
    $("#datePickerInicio").datepicker("show");
}
function exibeCalendarioFim() {
    $(function () {
        $("#datePickerFim").datepicker({
            language: "pt-BR",
            autoclose: true
        });
    });
    $("#datePickerFim").datepicker("show");
}
function pesquisaRecebimentos() {
    $.ajax({
        type: "POST",
        url: "/Caixa/Index",
        data: JSON.stringify(preencheDados()),
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: function (retorno) {
            if (retorno.Success) {
                constroiTabelaRecebimentos(retorno.Recebimentos);
//                var rowTotal = $("<tr>");
//                rowTotal.append("<td>TOTAL</td>");
//                rowTotal.append("<td></td>");
//                rowTotal.append("<td>" + calculaValorTotal() + "</td>");
//                rowTotal.append("</tr>");
//                $("#tableRecebimentos > tbody").append(rowTotal);
            }
            else {
                alert("Erro ao buscar recebimentos.");
            }
        }
    });
}
function preencheDados() {
    return {
        nomePessoa: $("#txtNomePessoa").val().toUpperCase(),
        dataInicio: $("#datePickerInicio").val(),
        dataFim: $("#datePickerFim").val()
    };
}
function constroiTabelaRecebimentos(recebimentos) {
    $("#tableRecebimentos > thead").empty();
    $("#tableRecebimentos > tbody").empty();
    if (recebimentos.length > 0) {
        var cabecalho = $("<tr>");
        cabecalho.append("<th class='col-sm-8'>Pessoa</th>");
        cabecalho.append("<th class='col-sm-2'>Data Pagamento</th>");
        cabecalho.append("<th class='col-sm-2'>Valor</th>");
        cabecalho.append("</tr>");
        $("#tableRecebimentos > thead").append(cabecalho);
        for (var i = 0; i < recebimentos.length; i++) {
            var row = $("<tr>");
            row.append("<td>" + recebimentos[i].Pessoa.Nome + "</td>");
            row.append("<td>" + retornaData(new Date(parseInt(recebimentos[i].DataPagamento.substr(6, 13)))) + "</td>");
            row.append("<td>" + recebimentos[i].Valor + "</td>");
            row.append("</tr>");
            $("#tableRecebimentos > tbody").append(row);
        }        
    }
}
function retornaData(data) {
    var dia = data.getDate();
    var mes = data.getMonth() + 1;
    var ano = data.getFullYear();
    return (dia < 10 ? "0" + dia : dia) + "/" + (mes < 10 ? "0" + mes : mes) + "/" + ano;
}
function calculaValorTotal() {
    var valorTotal = 0;
    $("#tableRecebimentos tr").each(function () {
        valorTotal += parseFloat($(this).find("td:eq(3)").text());
    });
    return valorTotal.toFixed(2);
}