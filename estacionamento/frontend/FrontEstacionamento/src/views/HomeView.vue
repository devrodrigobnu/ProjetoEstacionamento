<template>
  <div>
    <Header />
    <div class="container">
      <div class="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Placa</th>
              <th>Horário de Chegada</th>
              <th>Horário de Saída</th>
              <th>Duração</th>
              <th>Valor a Pagar</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="registro in registros" :key="registro.veiculo.placa">
              <td>{{ registro.veiculo.placa }}</td>
              <td>{{ formatarHora(registro.dataEntrada) }}</td>
              <td>
                <span v-if="registro.dataSaida">
                  {{ formatarHora(registro.dataSaida) }}
                </span>
                <span v-else>
                  Carro se encontra no pátio
                </span>
              </td>
              <td>{{ calcularDuracao(registro.dataEntrada, registro.dataSaida) }}</td>
              <td>{{ registro.valorTotal ? 'R$ ' + registro.valorTotal.toLocaleString('pt-BR', {
                minimumFractionDigits:
                  2, maximumFractionDigits: 2
              }) : 'Preço não calculado ainda' }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import Header from '../components/Header.vue';
import axios from 'axios';
import moment from 'moment';

async function calcularPreco(placa) {
  try {
    const response = await axios.get(`http://localhost:5001/api/preco/${placa}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response ? error.response.data : error.message);
  }
}

export default {
  name: 'HomeView',
  components: {
    Header
  },
  data() {
    return {
      registros: []
    };
  },
  methods: {
    async fetchRegistros() {
      try {
        const response = await axios.get('http://localhost:5001/api/estacionamentos/registros');
        const registros = response.data;
        this.registros = registros;
      } catch (error) {
        console.error(error.message);
      }
    }
    ,
    formatarHora(hora) {
      return hora ? moment(hora).format('DD/MM/YYYY HH:mm:ss') : 'N/A';
    },
    calcularDuracao(dataEntrada, dataSaida) {
      if (!dataSaida) {
        return;
      }
      const entrada = moment(dataEntrada);
      const saida = moment(dataSaida);
      const duracaoEmMilissegundos = saida.diff(entrada);
      return moment.utc(duracaoEmMilissegundos).format('HH:mm:ss');
    }
  },
  mounted() {
    this.fetchRegistros();
  }
};
</script>

<style>
html,
body {
  height: 100%;
  margin: 0;
}

.container {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.table-wrapper {
  flex: 1;
  overflow: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th,
td {
  padding: 20px;
  border: 1px solid #ddd;
  text-align: center;
  line-height: 1.5;
}

th {
  background-color: #f4f4f4;
}
</style>
