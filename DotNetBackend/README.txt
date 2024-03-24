NAPOMENA: sve je u jednom repozitoriju (za front-end)

Za zadatak:
- napravio sam MVC backend
- povezao sam frontend i backend
- zadovoljio sam sve "Restrikcije i napomene" (uvjeti u kodu)
- provjeren program

Nisam uspio deployati aplikaciju zbog internih grešaka u programu za deploy fazi

Models:
- Student.cs
- Professor.cs
- Subject.cs
- InstructionsDate.cs
- InstructionSession.cs
- AppDbContext.cs

Controllers:
- StudentController.cs
- ProfessorController.cs
- SubjectController.cs

OPISI (svojstva i metode):

MODELS:

Student.cs
Opis: Predstavlja entitet studenta u sustavu.
Svojstva:
Id: Jedinstveni identifikator studenta.
Email: E-adresa studenta.
Ime: Ime studenta.
Prezime: Prezime studenta.
Lozinka: Lozinka za račun studenta.
ProfilePictureUrl: URL profila slike studenta.

Professor.cs
Opis: Predstavlja entitet profesora u sustavu.
Svojstva:
Id: Jedinstveni identifikator profesora.
Email: E-adresa profesora.
Ime: Ime profesora.
Prezime: Prezime profesora.
Lozinka: Lozinka za račun profesora.
ProfilePictureUrl: URL profila slike profesora.
InstructionsCount: Broj instrukcija koje je profesor održao.
Subjects: Polje predmeta koje profesor predaje.

Subject.cs
Opis: Predstavlja entitet predmeta u sustavu.
Svojstva:
Id: Jedinstveni identifikator predmeta.
Title: Naslov predmeta.
Url: Jedinstveni URL identifikator predmeta.
Description: Opis predmeta.

InstructionsDate.cs
Opis: Predstavlja datum instrukcijske sesije između studenta i profesora.
Svojstva:
StudentId: Id studenta uključenog u instrukcijsku sesiju.
ProfessorId: Id profesora uključenog u instrukcijsku sesiju.
DateTime: Datum i vrijeme instrukcijske sesije.
Status: Stanje instrukcijske sesije.

InstructionSession.cs
- započeo za definiranje instrukcija

AppDbContext.cs
- podloga za povezivanje s bazom podataka
- implementirano u trenutni kod


CONTROLLERS:

StudentController.cs
Opis: Kontroler za upravljanje operacijama povezanim s studentima.
Metode:
Register: Registrira novog studenta u sustavu.
Login: Provjerava autentičnost studenta i omogućuje mu prijavu u sustav.
GetStudentByEmail: Dohvaća informacije o studentu putem e-pošte.
GetAllStudents: Dohvaća sve studente u sustavu.

ProfessorController.cs
Opis: Kontroler za upravljanje operacijama povezanim s profesorima.
Metode:
Register: Registrira novog profesora u sustavu.
Login: Provjerava autentičnost profesora i omogućuje mu prijavu u sustav.
GetProfessorByEmail: Dohvaća informacije o profesoru putem e-pošte.
GetAllProfessors: Dohvaća sve profesore u sustavu.

SubjectController.cs
Opis: Kontroler za upravljanje operacijama povezanim s predmetima.
Metode:
CreateSubject: Stvara novi predmet u sustavu.
GetSubjectByUrl: Dohvaća informacije o predmetu putem URL-a.
GetAllSubjects: Dohvaća sve predmete u sustavu.
ScheduleInstructionSession: Stvara instrukcijsku sesiju
