	--DATAS DE PAGAMENTO E LANÇAMENTO---------------------------------------------------------------------------------------------
	SELECT * FROM Taxa
	SELECT DISTINCT(CAST(c.DataPagamento AS DATE)) AS DataPagamento FROM Caixa c ORDER BY DataPagamento	
	SELECT DISTINCT(CAST(l.DataVencimento AS DATE)) AS DataVencimento FROM Lancamento l ORDER BY DataVencimento			
	------------------------------------------------------------------------------------------------------------------------------	
				
				
	--FLUXO DE CAIXA POR TAXA E DATA DE VENCIMENTO--------------------------------------------------------------------------------	
	SELECT
	UPPER(t.Nome) AS TAXA,
	--CONVERT(VARCHAR(10), l.DataVencimento, 103) AS VENCIMENTO,
	COUNT(*) AS QUANTIDADE,
	SUM(l.Valor) AS VALOR
	FROM Caixa c INNER JOIN LancamentoToCaixa lc ON c.CaixaId = lc.CaixaId
				 INNER JOIN Lancamento l ON l.LancamentoId = lc.LancamentoId
				 INNER JOIN Taxa t ON t.TaxaId = l.TaxaId
	WHERE 
	CONVERT(date, c.DataPagamento) > '2016-01-01' 
	--CONVERT(date, c.DataPagamento) IN ('2016-01-25')
	GROUP BY t.Nome
	--, l.DataVencimento
	ORDER BY 
	--l.DataVencimento,
	t.Nome
	------------------------------------------------------------------------------------------------------------------------------
	
	
	--À RECEBER-------------------------------------------------------------------------------------------------------------------
	SELECT 
	UPPER(p.Nome) AS NOME,
	UPPER(p.Lugar) AS LUGAR
	FROM Pessoa p WHERE p.PessoaId NOT IN 
	(
		SELECT PessoaId FROM Lancamento WHERE DataVencimento = '2016-01-10'
		AND Pago > 0
	)
	OR p.PessoaId IN 
	(
		SELECT PessoaId FROM Lancamento WHERE DataVencimento = '2016-01-10'
		AND Pago = 0
	)	
	ORDER BY p.Lugar, p.Nome
	------------------------------------------------------------------------------------------------------------------------------
	
	
	--SÓCIOS QUITES---------------------------------------------------------------------------------------------------------------
	SELECT
	UPPER(p.Nome) AS NOME,
	UPPER(p.Lugar) AS LUGAR
	FROM Pessoa p WHERE p.PessoaId NOT IN
	(
		SELECT 
		PessoaId
		FROM Pessoa p WHERE p.PessoaId NOT IN 
		(
			SELECT PessoaId FROM Lancamento WHERE DataVencimento = '2016-01-10'
			AND Pago > 0
		)
		OR p.PessoaId IN 
		(
			SELECT PessoaId FROM Lancamento WHERE DataVencimento = '2016-01-10'
			AND Pago = 0
		)
	)
	ORDER BY p.Lugar, p.Nome
	------------------------------------------------------------------------------------------------------------------------------
	

	