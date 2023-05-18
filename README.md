# SchoolManager
API de gerenciamento escolar
<br><br><br>
## 🔋 STATUS 
![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)
<br><br><br>


## 🛠️ FERRAMENTAS 
![AutoMapper](https://img.shields.io/badge/Biblioteca-AutoMapper-blue)
![EF Core](https://img.shields.io/badge/Framework-EF%20Core-purple)
![Newtonsoft.Json](https://img.shields.io/badge/Biblioteca-Newtonsoft.Json-orange)
![MySQL](https://img.shields.io/badge/Banco%20de%20dados-MySQL-blue)
![C#](https://img.shields.io/badge/Linguagem-C%23-blue)
![.NET](https://img.shields.io/badge/Plataforma-.NET-blue)
[![Postman](https://img.shields.io/badge/Ferramenta-Postman-orange?logo=postman)](https://www.postman.com/)
[![Regex](https://img.shields.io/badge/Ferramenta-Regex-blueviolet)](https://regex101.com/)

<br><br><br>



## 📝 DESCRIÇÃO
API RESTfull e Fluent API que se comunica com o banco de dados MySQL com entidades de relacionamentos a partir de rotas que realizam CRUD

##### Entidades
- Aluno (Student)
- Turma (Class)
- Disciplina (Subject)
- Professor (Teacher)
- Aula (ClassSchedule)

##### Relacionamentos
|  1 : 1 |  1 : N |  N : N |      
| -------- | -------- | -------- |
| Aluno : Turma  | Turma :  Disciplina   | Aula = TeacherId : ClassId   |
| Professor : Disciplina   | Professor  :  Turma    |  

##### Relação de dependência entre entidades
- Para que seja criado um Aluno, Turma precisa existir antes
- Para que seja criado um Professor, a Disciplina precisa existir antes
- Para que seja criado uma Aula, Professor e Turma precisa existir antes


<br><br>
 ### ℹ️ INFORMAÇÕES
 * As entidades possuem camadas de DTOs e são mapeadas pela biblioteca AutoMapper
 * As entidades possuem rotas em CRUD
   * POST, GET, GET {id}, PATCH, DELETE
   * Para criar a rota PATCH das entidades foi usado a biblioteca Newtonsoft.Json
   * Rotas do tipo GET possuem paginação
      * se não for especificado o skip/take, por padrão começa trazendo de 0 a 10 recursos 
 * A prática de Fluent API foi usada apenas no relacionamento N:N
 * A prática de API RESTfull estão em todos os controller
 * A aplicação possui controle de exceção
 * Antes dos dados serem mapeados para o bando de dados, eles foram validados com [data annotation] na camada Model
 * O projeto possui camdas de Repository e Service para serem aplicados no Controller
 

<br><br>
> **Documentação:** Este projeto possui documentação de suas rotas pelo swegger






<br><br><br>
## 👩‍💻 Autora
Larissa Leal 
<br><br>
[![Badge](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/larissa-leal-dias-408455157/)

