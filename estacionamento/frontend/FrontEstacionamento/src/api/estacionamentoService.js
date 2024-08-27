import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5001/api',
    headers: {
        'Content-Type': 'application/json',
    },
});

export async function registrarEntrada(placa, dataEntrada, valorHora) {
    try {
        await api.post('estacionamentos/entrada', {
            Veiculo: { Placa: placa },
            DataEntrada: dataEntrada,
            Preco: { ValorHora: valorHora }
        });
    } catch (error) {
        throw new Error(error.response ? error.response.data : error.message);
    }
}

export async function registrarSaida(placa, horaSaida, valorHora) {
    try {
        await api.post('estacionamentos/saida', {
            Veiculo: { Placa: placa },
            DataSaida: horaSaida,
            Preco: { ValorHora: valorHora }
        });
    } catch (error) {
        throw new Error(error.response ? error.response.data : error.message);
    }
}

export async function deletarVeiculo(placa) {
    try {
        const response = await api.delete(`estacionamentos/deletar/${placa}`);
        return response.data;
    } catch (error) {
        throw new Error(error.response ? error.response.data : error.message);
    }
}

export async function atualizarVeiculo(placaAntiga, placaNova) {
    try {
        const response = await api.put('estacionamentos/atualizar', null, {
            params: { placaAntiga, placaNova }
        });
        return response.status === 200;
    } catch (error) {
        throw new Error(error.response ? error.response.data : error.message);
    }
}

export async function calcularPreco(placa) {
    try {
        const response = await api.get(`estacionamentos/preco/${placa}`);
        return response.data;
    } catch (error) {
        throw new Error(error.response ? error.response.data : error.message);
    }
}