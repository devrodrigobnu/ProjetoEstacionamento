<template>
  <div>
    <header>
      <button class="entrada-button" @click="entradaRegistro">REGISTRAR ENTRADA</button>
      <button class="deletar-button" @click="deletarRegistro">DELETAR REGISTRO</button>
      <div class="header-content">
        <div class="title-container">
          <h1>Controle de Estacionamento</h1>
          <h2 class="date-time">{{ dataFormatada }} - {{ timeFormatado }}</h2>
        </div>
      </div>
      <button class="calcular-button" @click="calcularRegistro">VALOR A PAGAR</button>
      <button class="atualizar-button" @click="atualizarRegistro">ATUALIZAR REGISTRO</button>
      <button class="saida-button" @click="saidaRegistro">REGISTRAR SAÍDA</button>
    </header>

    <div v-if="entradaDialog" class="modal-overlay" @click.self="closeEntradaDialog">
      <div class="modal-content">
        <span class="modal-close" @click="closeEntradaDialog">&times;</span>
        <h2>Registrar Entrada</h2>
        <input v-model="placa" placeholder="Digite a placa" class="input-placa" />
        <div class="modal-date-time">
          <p>{{ dataFormatadaModal }} - {{ horaFormatadaModal }}</p>
        </div>
        <div class="modal-buttons">
          <button @click="registerEntrada" class="register-button">Registrar</button>
          <button @click="closeEntradaDialog" class="cancel-button">Cancelar</button>
        </div>
      </div>
    </div>

    <div v-if="saidaDialog" class="modal-overlay" @click.self="closeSaidaDialog">
      <div class="modal-content">
        <span class="modal-close" @click="closeSaidaDialog">&times;</span>
        <h2>Registrar Saída</h2>
        <input v-model="placa" placeholder="Digite a placa" class="input-placa" />
        <div class="modal-date-time">
          <p>{{ dataFormatadaModal }} - {{ horaFormatadaModal }}</p>
        </div>
        <div class="modal-buttons">
          <button @click="registerSaida" class="register-button">Registrar</button>
          <button @click="closeSaidaDialog" class="cancel-button">Cancelar</button>
        </div>
      </div>
    </div>

    <div v-if="deletarDialog" class="modal-overlay" @click.self="closeDeletarDialog">
      <div class="modal-content">
        <span class="modal-close" @click="closeDeletarDialog">&times;</span>
        <h2>Deletar Registro</h2>
        <input v-model="placa" placeholder="Digite a placa para deletar" class="input-placa" />
        <div class="modal-date-time">
          <p>{{ dataFormatadaModal }} - {{ horaFormatadaModal }}</p>
        </div>
        <div class="modal-buttons">
          <button @click="confirmarDeletar" class="deletar-button-modal">Deletar</button>
          <button @click="closeDeletarDialog" class="cancel-button">Cancelar</button>
        </div>
      </div>
    </div>

    <div v-if="atualizarDialog" class="modal-overlay" @click.self="closeAtualizarDialog">
      <div class="modal-content">
        <span class="modal-close" @click="closeAtualizarDialog">&times;</span>
        <h2>Atualizar Registro</h2>
        <input v-model="placaAntiga" placeholder="Digite a placa antiga" class="input-placa" />
        <input v-model="placaNova" placeholder="Digite a placa nova" class="input-placa" />
        <div class="modal-date-time">
          <p>{{ dataFormatadaModal }} - {{ horaFormatadaModal }}</p>
        </div>
        <div class="modal-buttons">
          <button @click="registerAtualizar" class="atualizar-button-modal">Atualizar</button>
          <button @click="closeAtualizarDialog" class="cancel-button">Cancelar</button>
        </div>
      </div>
    </div>
  </div>

  <div v-if="calcularDialog" class="modal-overlay" @click.self="closeCalcularDialog">
    <div class="modal-content">
      <span class="modal-close" @click="closeCalcularDialog">&times;</span>
      <h2>Calcular Valor a Pagar</h2>
      <input v-model="placa" placeholder="Digite a placa" class="input-placa" />
      <div class="modal-date-time">
        <p>{{ dataFormatadaModal }} - {{ horaFormatadaModal }}</p>
      </div>
      <div class="modal-buttons">
        <button @click="calcularValorPagar" class="calcular-button-modal">Calcular</button>
        <button @click="closeCalcularDialog" class="cancel-button">Cancelar</button>
      </div>
    </div>
  </div>

</template>

<script>
import moment from 'moment';
import { registrarEntrada, registrarSaida, deletarVeiculo, atualizarVeiculo, calcularPreco } from '../api/estacionamentoService';

export default {
  name: 'Header',
  data() {
    return {
      currentDateTime: new Date(),
      entradaDialog: false,
      saidaDialog: false,
      deletarDialog: false,
      atualizarDialog: false,
      calcularDialog: false,
      placa: '',
      placaAntiga: '',
      placaNova: '',
      valorCalculado: null
    };
  },
  computed: {
    dataFormatada() {
      return this.currentDateTime.toLocaleDateString('pt-BR');
    },
    timeFormatado() {
      const date = this.currentDateTime;
      return `${String(date.getHours()).padStart(2, '0')}:${String(date.getMinutes()).padStart(2, '0')}:${String(date.getSeconds()).padStart(2, '0')}`;
    },
    dataFormatadaModal() {
      return this.currentDateTime.toLocaleDateString('pt-BR');
    },
    horaFormatadaModal() {
      const date = this.currentDateTime;
      return `${String(date.getHours()).padStart(2, '0')}:${String(date.getMinutes()).padStart(2, '0')}:${String(date.getSeconds()).padStart(2, '0')}`;
    }
  },
  methods: {
    updateDateTime() {
      this.currentDateTime = new Date();
    },

    entradaRegistro() {
      this.placa = '';
      this.updateDateTime();
      this.entradaDialog = true;
    },
    closeEntradaDialog() {
      this.entradaDialog = false;
    },
    async registerEntrada() {
      try {
        const dataFormatada = moment(this.currentDateTime).format('YYYY-MM-DDTHH:mm:ss');
        await registrarEntrada(this.placa, dataFormatada);
        this.closeEntradaDialog();
        window.location.reload();
      } catch (error) {
        alert(error.message);
      }
    },

    saidaRegistro() {
      this.placa = '';
      this.updateDateTime();
      this.saidaDialog = true;
    },
    closeSaidaDialog() {
      this.saidaDialog = false;
    },
    async registerSaida() {
      try {
        const dataFormatada = moment(this.currentDateTime).format('YYYY-MM-DDTHH:mm:ss');
        await registrarSaida(this.placa, dataFormatada);
        this.closeSaidaDialog();
        window.location.reload();
      } catch (error) {
        alert(error.message);
      }
    },

    deletarRegistro() {
      this.placa = '';
      this.updateDateTime();
      this.deletarDialog = true;
    },
    closeDeletarDialog() {
      this.deletarDialog = false;
    },
    async confirmarDeletar() {
      try {
        await deletarVeiculo(this.placa);
        this.closeDeletarDialog();
        window.location.reload();
      } catch (error) {
        alert(error.message);
      }
    },

    atualizarRegistro() {
      this.placaAntiga = '';
      this.placaNova = '';
      this.updateDateTime();
      this.atualizarDialog = true;
    },
    closeAtualizarDialog() {
      this.atualizarDialog = false;
    },
    async registerAtualizar() {
      try {
        await atualizarVeiculo(this.placaAntiga, this.placaNova);
        this.closeAtualizarDialog();
        window.location.reload();
      } catch (error) {
        alert(error.message);
      }
    },

    calcularRegistro() {
      this.placa = '';
      this.valorCalculado = null;
      this.updateDateTime();
      this.calcularDialog = true;
    },
    closeCalcularDialog() {
      this.calcularDialog = false;
    },
    async calcularValorPagar() {
      try {
        this.valorCalculado = await calcularPreco(this.placa);
        window.location.reload();
      } catch (error) {
        alert(error.message);
      }
    }
  },
  mounted() {
    this.updateDateTime();
    this.interval = setInterval(this.updateDateTime, 1000);
  },
  beforeUnmount() {
    clearInterval(this.interval);
  }
};
</script>

<style>
header {
  background-color: #f8f9fa;
  border-bottom: 1px solid #dee2e6;
  padding: 10px 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: relative;
}

button {
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
  width: 150px;
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
}

.entrada-button {
  background-color: #28a745;
}

.entrada-button:hover {
  background-color: #218838;
}

.saida-button {
  background-color: #dc3545;
}

.saida-button:hover {
  background-color: #c82333;
}

.header-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  flex: 1;
}

.title-container {
  text-align: center;
}

h1 {
  margin: 0;
}

h2.date-time {
  font-size: 16px;
  margin: 5px 0 0 0;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 20px;
  border-radius: 5px;
  width: 400px;
  text-align: center;
  position: relative;
}

.modal-close {
  position: absolute;
  top: 10px;
  right: 10px;
  cursor: pointer;
  font-size: 24px;
}

.input-placa {
  width: calc(100% - 20px);
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 4px;
  border: 1px solid #dee2e6;
}

.modal-date-time {
  margin-bottom: 20px;
}

.modal-buttons {
  display: flex;
  justify-content: center;
  gap: 10px;
}

.register-button {
  background-color: #28a745;
}

.register-button:hover {
  background-color: #218838;
}

.cancel-button {
  background-color: #dc3545;
}

.cancel-button:hover {
  background-color: #c82333;
}

.deletar-button {
  background-color: blue;
  margin: 10px
}

.atualizar-button {
  background-color: orange;
  margin: 10px;
}

.deletar-button-modal {
  background-color: blue;
}

.atualizar-button-modal {
  background-color: orange;
}

.calcular-button {
  background-color: aqua;
}

.calcular-button-modal {
  background-color: green;
}
</style>
