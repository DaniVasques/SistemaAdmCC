# SistemaAdmCC

<p align="center">
  Sistema administrativo modular desenvolvido em <strong>.NET 10</strong> para apoiar a gestão da rede <strong>Adm C+C</strong>.
</p>

<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-10-512BD4" />
  <img alt="C#" src="https://img.shields.io/badge/C%23-Backend-239120" />
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91" />
  <img alt="Entity Framework Core" src="https://img.shields.io/badge/EF%20Core-SQL%20Server-0C54C2" />
  <img alt="Status" src="https://img.shields.io/badge/Status-Em%20evolu%C3%A7%C3%A3o-orange" />
</p>

---

## Sobre o projeto

O *SistemaAdmCC* foi projetado como uma solução back-end robusta e organizada para centralizar processos administrativos, operacionais e estratégicos da estrutura *Adm C+C*.

A proposta do projeto foi construir uma base consistente, modular e escalável, com foco em:

- organização arquitetural
- separação clara de responsabilidades
- consistência entre domínio, infraestrutura e API
- proteção de regras de negócio no back-end
- documentação da API
- preparação para futura integração com front-end em Angular

Mais do que apenas expor CRUDs, a intenção foi estruturar um sistema com *módulos de domínio bem definidos*, preparado para crescer de forma segura.

---

## Objetivo

O objetivo do sistema é apoiar a gestão de:

- associados
- equipes
- conexões estratégicas
- seguros
- reuniões C+C
- cargos e lideranças
- estratégia e grupamentos
- perfil público
- visitantes e substitutos
- auditoria de logs

---

## Arquitetura da solução

A solution foi organizada em camadas para manter clareza, desacoplamento e facilidade de manutenção.

### Projetos principais

#### AdmCC.Api
Camada de apresentação da API.

Responsável por:
- controllers
- requests
- responses
- configurações
- documentação OpenAPI/Scalar

#### AdmCC.Domain
Camada de domínio.

Responsável por:
- entities
- enums
- interfaces
- modelagem central do negócio

#### AdmCC.InfraData
Camada de infraestrutura e persistência.

Responsável por:
- DbContext
- mappings
- repositories
- services
- dependency injection
- migrations

### Estrutura já preparada para expansão

A solution também possui a base dos projetos:

- UsuarioAdmCC.Api
- UsuarioAdmCC.Domain
- UsuarioAdmCC.InfraData

> Neste momento, o foco principal implementado está no módulo *AdmCC*.

---

## Organização modular

O sistema foi dividido em módulos para representar melhor o domínio e facilitar evolução futura.

### Módulos implementados / estruturados

- CadastroBase
- Equipes
- ConexoesParcerias
- Seguro
- OperacaoSemanalReuniaoCC
- CargosLiderancas
- Estrategia
- PerfilPublico
- VisitantesSubstitutos
- RelatoriosAuditorias

Essa mesma separação foi refletida em:

- entities
- enums
- interfaces
- mappings
- repositories
- services
- requests
- responses
- controllers

---

## Tecnologias utilizadas

- *C#*
- *.NET 10*
- *ASP.NET Core Web API*
- *Entity Framework Core*
- *SQL Server*
- *OpenAPI*
- *Scalar*
- *Injeção de Dependência*
- *Arquitetura em camadas*
- *Arquitetura modular por domínio*

---

## Funcionalidades por módulo

## CadastroBase
Módulo responsável pelo núcleo do associado.

### Implementado
- cadastro de associado
- listagem de associados
- consulta por id
- atualização
- exclusão
- alteração de status
- gestão de anuidade

### Observações
- o associado é tratado como agregado principal
- empresa e endereço foram mantidos como dependências do agregado no escopo atual

---

## Equipes

### Implementado
- cadastro de equipes
- listagem
- consulta por id
- atualização
- exclusão
- ocorrências de reunião
- controle de presença

### Observações
- módulo está bem estruturado
- parte de parametrização de pontuação ficou reservada para evolução futura

---

## ConexoesParcerias

### Implementado
- registro de conexões estratégicas
- listagem e consulta
- atualização
- exclusão
- validação de negócio recebido
- registro de parcerias

### Destaque
Este módulo já demonstra um fluxo de negócio mais rico do que um CRUD simples.

---

## Seguro

### Implementado
- seguro do associado
- beneficiários
- contato de emergência
- solicitação de alteração de beneficiário
- consentimento LGPD

### Situação
Módulo completo para o recorte atual.

---

## OperacaoSemanalReuniaoCC

### Implementado
- ciclos semanais
- reuniões C+C
- fluxo principal do módulo semanal
- validações de reunião
- proteção da regra para garantir que apenas participante válido possa validar

### Situação
Módulo quase completo no escopo atual.

---

## CargosLiderancas

### Implementado
- catálogo principal de cargos de liderança

### Observações
O escopo atual foi mantido de forma honesta, sem expor vínculos auxiliares ainda não sustentados por caso de uso completo.

---

## Estrategia

### Implementado
- clusters
- atuações específicas
- grupamentos estratégicos

### Observações
Parte da expansão estratégica foi mantida como evolução futura.

---

## PerfilPublico

### Implementado
- perfil do associado
- mídias associadas

### Situação
Módulo quase completo no recorte atual.

---

## VisitantesSubstitutos

### Implementado
- visitante externo
- visita interna
- substituto associado
- substituto externo

### Destaque
O módulo foi amadurecido no backend com suporte em:
- domínio
- repository
- service
- controller

---

## RelatoriosAuditorias

### Implementado
- logs de auditoria

### Observações
O módulo foi ajustado para refletir somente o que realmente entrega hoje, evitando prometer funcionalidades ainda não implementadas.

---

## Estrutura da API

A camada AdmCC.Api foi organizada para separar bem o contrato da API do domínio interno.

### Estrutura principal
- Configurations
- Controllers
- Models
  - Requests
  - Responses

### Padrão adotado
- controllers finos
- uso de services
- sem acesso direto ao DbContext
- sem acesso direto a repositories
- requests e responses separados das entities do domínio

---

## Banco de dados

O projeto utiliza *SQL Server* com *Entity Framework Core*.

### Estrutura persistida
A persistência foi organizada de forma consistente entre:

- entities
- AdmCCContext
- mappings
- migration inicial

### Infraestrutura
- DbContext configurado
- ApplyConfigurationsFromAssembly(...)
- repositories por módulo
- services por módulo
- migration inicial alinhada ao núcleo persistido

---

## Injeção de dependência

A injeção de dependência foi configurada para registrar corretamente:

- DbContext
- repositories
- services
- IUnitOfWork

Isso permite que a API resolva corretamente seus controllers e serviços sem acoplamento indevido.

---

## Documentação da API

A API está documentada com *OpenAPI + Scalar*.

### Acesso local
```txt
http://localhost:5153/scalar
