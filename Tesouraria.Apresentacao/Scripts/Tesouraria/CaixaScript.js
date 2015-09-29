$(function () {
    RegistraEventos();
    //SelecionaTodasAsTaxas();
});

function RegistraEventos() {
    onSalva();
    onVolta();
    onNovo();
    onPesquisa();
    onSelecionaTaxa();
    CalculaValorTotal();
}

function onSalva() {
    $("#btSalvar").click(function () {
        $("#form").submit();
    });
}

function onVolta() {
    $("#btVoltar").click(function () {
        window.location.href = '/Caixa/Index';
    });
}

function onNovo() {
    $("#btNovo").click(function () {
        window.location.href = "/Caixa/Create";
    });
}

function onPesquisa() {
    $("#btPesquisar").click(function () {
        $("form").submit();
    });
}

function onSelecionaTaxa() {
    $("#tableTaxas").on("change", "input[type='checkbox']", function () {
        CalculaValorTotal();
    });
}

function SelecionaTodasAsTaxas() {
    $("#tableTaxas").find("input[type='checkbox']").each(function () {
        $(this).prop('checked', true);
        CalculaValorTotal();
    });
}

function CalculaValorTotal() {
    resultado = 0;
    $("#tableTaxas").find("input[type='checkbox']:checked").each(function () {
        resultado += parseFloat($(this).closest("tr").find("td").eq(2).html().replace(",", "."));
    });
    $("#Valor").val(resultado.toString().replace(".", ","));
}