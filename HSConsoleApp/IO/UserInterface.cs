using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;
using HSLibrary.Models.Dinghy;
using HSLibrary.Interfaces;
using HSLibrary.Services;
using HSConsoleApp.IO;
using System.Reflection.Metadata;


namespace HSConsoleApp
{
    public class UserInterface
    {
        private Input _input;
        private Output _output;

        private IBlogRepository _blogRepository;
        private IBookingRepository _bookingRepository;
        private IDinghyRepository _dinghyRepository;
        private IEventRepository _eventRepository;
        private IMemberRepository _memberRepository;
        private ITeamRepository _teamRepository;

        private Member _currentUser;
        private bool _isLoggedIn
        {
            get
            {
                return _currentUser != null;
            }
        }

        public UserInterface()
        {
            _blogRepository = new BlogRepository();
            _bookingRepository = new BookingRepository();
            _dinghyRepository = new DinghyRepository();
            _eventRepository = new EventRepository();
            _memberRepository = new MemberRepository();
            _teamRepository = new TeamRepository();
            _input = new Input();
            _output = new Output();
        }

        private void ManageCurrentUser()
        {
            if (!_isLoggedIn)
            {
                Login();
            }
            else
            {
                Logout();
            }
        }
        private void Login()
        {
            _currentUser = DisplayAndSelectFromList(_memberRepository.GetAll());
        }
        private void Logout()
        {
            _currentUser = null;
        }

        public void OpenMenu()
        {
            //1. Manage current user
            //2. - 7. Manage the repositories
            //Q. Quit (break;)
            string answer;
            string question;
            while (true)
            {
                question = $"Bruger: " + (_currentUser == null ? "Ingen" : _currentUser.Name);
                question += $"\nHvad ønsker du at gøre?";
                question += $"\n\t1. " + (_currentUser == null ? "Login" : "Logud");
                if (_isLoggedIn)
                {
                    question += $"\n\t2. Åben Blogkataloget";
                    question += $"\n\t3. Åben Bookingkataloget";
                    question += $"\n\t4. Åben Jollekataloget";
                    question += $"\n\t5. Åben Begivenhedskataloget";
                    question += $"\n\t6. Åben Medlemskataloget";
                    question += $"\n\t7. Åben Holdkataloget";
                }
                else
                {
                    question += $"\n\t2. Opret Bruger";
                }
                question += $"\n\tq. AFSLUT";
                _output.Write(question);
                answer = _input.Read();
                switch (answer)
                {
                    case "1":
                        ManageCurrentUser();
                        break;
                    case "2":
                        if (_isLoggedIn)
                        {
                            OpenSubmenuBlogRepository();
                        }
                        else
                        {
                            Member newMember = CreateNewMember();
                            _memberRepository.Add(newMember);
                            _currentUser = newMember;
                        }
                        break;
                    case "3":
                        if (_isLoggedIn) OpenSubmenuBookingRepository();
                        break;
                    case "4":
                        if (_isLoggedIn) OpenSubmenuDinghyRepository();
                        break;
                    case "5":
                        if (_isLoggedIn) OpenSubmenuEventRepository();
                        break;
                    case "6":
                        if (_isLoggedIn) OpenSubmenuMemberRepository();
                        break;
                    case "7":
                        if (_isLoggedIn) OpenSubmenuTeamRepository();
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }

        private T DisplayAndSelectFromList<T>(List<T> list)
        {
            _output.Write("Vælg fra følgende liste:");
            _output.DisplayList(list);
            return _input.SelectFromList(list);
        }

        private bool GetYesNo(string question)
        {
            _output.Write($"{question} (J/N)");
            string answer = _input.Read();
            while (answer.ToUpper() != "J" && answer.ToUpper() != "N")
            {
                _output.Write($"{question} (J/N)");
                answer = _input.Read();
            }
            return answer.ToUpper() == "J";
        }

        private bool IsPermittedUser(Member user)
        {
            return _currentUser.IsAdmin || _currentUser == user;
        }

        private void OpenSubmenuBlogRepository()
        {
            string answer;
            string question = "";
            question += $"Hvad ønsker du at gøre i dette katalog?";
            question += $"\n\t1. Opret nyt BlogOpslag";
            question += $"\n\t2. Slet BlogOpslag";
            question += $"\n\t3. Vis specifik BlogPost";
            question += $"\n\t4. Vis alle BlogOpslag";
            question += $"\n\t5. Vis BlogOpslag fra dato";
            question += $"\n\t6. Vis BlogOpslag fra medlem";
            question += $"\n\tq. TILBAGE";
            while (true)
            {
                _output.Write(question);
                answer = _input.Read();
                Blog blog;
                switch (answer)
                {
                    case "1"://Opret nyt BlogOpslag
                        _output.Write("Hvad skal bloggens title være?");
                        string title = _input.Read();
                        _output.Write("Hvad skal texten være?");
                        string text = _input.Read();
                        blog = new Blog(_currentUser, title, text);
                        _blogRepository.Add(blog);
                        break;
                    case "2"://Slet BlogOpslag
                        _output.Write("Hvilken Blog vil du slette?");
                        blog = DisplayAndSelectFromList(_blogRepository.GetAll());
                        if (IsPermittedUser(blog.PostedBy))
                        {
                            _blogRepository.Remove(blog.Id);
                            _output.Write("Bloggen er blevet slettet.");
                        }
                        else
                        {
                            _output.Write("For at slette denne blog skal du være admin eller poster.");
                        }
                        break;
                    case "3"://Vis specifikt BlogPost
                        _output.Write("Hvilken Blog vil du se?");
                        blog = DisplayAndSelectFromList(_blogRepository.GetAll());
                        _output.Write($"{blog}\n{blog.Text}");
                        if (GetYesNo("Ønsker du at ændre teksten?"))
                        {
                            blog.EditText(_input.Read());
                        }
                        break;
                    case "4"://Vis alle BlogOpslag
                        _output.Write(_blogRepository.ToString());
                        break;
                    case "5"://Vis BlogOpslag fra dato
                        _output.Write("Hvilken Dato vil du se? (Brug formatet \"dd-mm-yyyy\")");
                        DateOnly date = DateOnly.Parse(_input.Read());
                        _output.DisplayList(_blogRepository.GetAllOnDate(date));
                        break;
                    case "6"://Vis BlogOpslag fra medlem
                        _output.Write("Hvilken Medlems posts vil du gerne finde?");
                        Member member = DisplayAndSelectFromList(_memberRepository.GetAll());
                        _output.DisplayList(_blogRepository.GetAllByMember(member));
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }
        private void OpenSubmenuBookingRepository()
        {
            string answer;
            string question = "";
            question += $"Hvad ønsker du at gøre i dette katalog?";
            question += $"\n\t1. Opret ny booking";
            question += $"\n\t2. Slet booking";
            question += $"\n\t3. Vis specifik booking";
            question += $"\n\t4. Vis alle bookinger";
            question += $"\n\t5. Vis alle bookinger fra dato";
            question += $"\n\t6. Vis alle bookinger for bestemt medlem";
            question += $"\n\t7. Vis alle bookinger for bestemt jolle";
            question += $"\n\tq. TILBAGE";
            while (true)
            {
                _output.Write(question);
                answer = _input.Read();
                Booking booking;
                Dinghy dinghy;
                switch (answer)
                {
                    case "1": //opret ny booking
                        _output.Write("Hvilken dato vil du booke den? (Brug formatet \"dd-mm-yyyy hh:mm:ss\")");
                        DateTime bookingdate = DateTime.Parse(_input.Read());
                        _output.Write("Hvor længe vil du booke den? (Brug formatet \"hh:mm:ss\")");
                        TimeSpan timespan = TimeSpan.Parse(_input.Read());
                        _output.Write("Hvilken jolle vil du booke?");
                        dinghy = DisplayAndSelectFromList(_dinghyRepository.GetAll());

                        booking = new Booking(bookingdate, timespan, _currentUser, dinghy);
                        _bookingRepository.Add(booking);
                        break;
                    case "2": //slet booking
                        _output.Write("Hvilken booking vil du slette?");
                        booking = DisplayAndSelectFromList(_bookingRepository.GetAll());
                        if (IsPermittedUser(booking.BookedBy))
                        {
                            _bookingRepository.Remove(booking.Id);
                            _output.Write("Bookingen er blevet slettet.");
                        }
                        else
                        {
                            _output.Write("For at slette denne booking skal du være admin eller selv have oprettet den.");
                        }
                        break;
                    case "3": //Vis specifik booking
                        _output.Write("Hvilken Booking vil du se?");
                        booking = DisplayAndSelectFromList(_bookingRepository.GetAll());
                        _output.Write($"{booking}");
                        break;
                    case "4": //Vis alle bookinger
                        _output.Write(_bookingRepository.ToString());
                        break;
                    case "5": //Vis bookinger fra dato
                        _output.Write("Hvilken Dato vil du se? (Brug formatet \"dd-mm-yyyy\")");
                        DateOnly date = DateOnly.Parse(_input.Read());
                        _output.DisplayList(_bookingRepository.GetAllOnDate(date));
                        break;
                    case "6": //Vis bookinger for bestemt medlem
                        _output.Write("Hvilken Medlems booking vil du gerne finde?");
                        Member member = DisplayAndSelectFromList(_memberRepository.GetAll());
                        _output.DisplayList(_bookingRepository.GetAllByMember(member));
                        break;
                    case "7": //Vis bookinger for bestemt jolle
                        _output.Write("Hvilken jolles booking vil du gerne finde?");
                        dinghy = DisplayAndSelectFromList(_dinghyRepository.GetAll());
                        _output.DisplayList(_bookingRepository.GetAllByDinghy(dinghy));
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }
        private void OpenSubmenuDinghyRepository()
        {
            string answer;
            string question = "";
            question += $"Hvad ønsker du at gøre i dette katalog?";
            question += $"\n\t1. Tilføj jolle";
            question += $"\n\t2. Slet jolle";
            question += $"\n\t3. Vis specifik jolle";
            question += $"\n\t4. Vis alle joller";
            question += $"\n\t5. Vis alle joller som skal repareres";
            question += $"\n\t6. Vis alle sejlklare joller";
            question += $"\n\t7. Vis alle joller fra model";
            question += $"\n\tq. TILBAGE";

            while (true)
            {
                _output.Write(question);
                answer = _input.Read();

                Dinghy dinghy;
                List<DinghyModel> models = new List<DinghyModel> {
                            DinghyModel.Wayfarer,
                            DinghyModel.Tera,
                            DinghyModel.Feva,
                            DinghyModel.Europajolle,
                            DinghyModel.Lynæs,
                            DinghyModel.Laserjolle,
                            DinghyModel.Optimistjolle,
                            DinghyModel.Snipejolle
                };
                DinghyModel model;
                switch (answer)
                {
                    case "1": //opret jolle
                        _output.Write("Hvilken type jolle skal den være?");
                        model = DisplayAndSelectFromList(models);
                        _output.Write("Hvilke dele skal jollen havde?");
                        string components = _input.Read();
                        dinghy = new Dinghy(model, components);
                        _dinghyRepository.Add(dinghy);
                        break;
                    case "2": // slet jolle
                        _output.Write("Hvilken jolle vil du slette?");
                        dinghy = DisplayAndSelectFromList(_dinghyRepository.GetAll());
                        if (_currentUser.IsAdmin)
                        {
                            _dinghyRepository.Remove(dinghy.Id);
                            _output.Write("Jollen er blevet slettet.");
                        }
                        else
                        {
                            _output.Write("For at slette denne jolle skal du være admin.");
                        }
                        break;
                    case "3": //Vis specifik jolle
                        _output.Write("Hvilken begivenhed vil du se?");
                        dinghy = DisplayAndSelectFromList(_dinghyRepository.GetAll());
                        _output.Write($"{dinghy.ToString()}");
                        break;
                    case "4": //Vis alle joller
                        _output.Write(_dinghyRepository.ToString());
                        break;
                    case "5": //Vis alle joller som skal repareres
                        _output.Write("List over alle joller der skal repareres");
                        _output.DisplayList(_dinghyRepository.GetAllNeedingRepairs());
                        break;
                    case "6": //Vis alle sejlklare joller
                        _output.Write("List over alle joller der er sejlklare");
                        _output.DisplayList(_dinghyRepository.GetAllSeaWorthy());
                        break;
                    case "7": //Vis alle joller fra model
                        _output.Write("Hvilken type jolle skal den være?");
                        model = DisplayAndSelectFromList(models);
                        _output.DisplayList(_dinghyRepository.GetAllOfModel(model));
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }
        private void OpenSubmenuEventRepository()
        {
            string answer;
            string question = "";
            question += $"Hvad ønsker du at gøre i dette katalog?";
            question += $"\n\t1. Tilføj begivenhed";
            question += $"\n\t2. Slet begivenhed";
            question += $"\n\t3. Vis specifik begivenhed";
            question += $"\n\t4. Vis alle begivenheder";
            question += $"\n\t5. Vis alle begivenhed(er) fra dato";
            question += $"\n\t6. Vis alle begivenhed(er) fra medlem";
            question += $"\n\tq. TILBAGE";
            while (true)
            {
                _output.Write(question);
                answer = _input.Read();
                Event @event;
                switch (answer)
                {
                    case "1":
                        _output.Write("Hvornår er begivenheden? (Brug formatet \"dd-mm-yyyy hh:mm:ss\")");
                        DateTime dateTime = DateTime.Parse(_input.Read());
                        _output.Write("Giv begivenheden en titel");
                        string title = _input.Read();
                        _output.Write("Beskrive begivenheden");
                        string description = _input.Read();
                        if (GetYesNo("Skal der være et maksimum af deltagere?"))
                        {
                            _output.Write("Hvor mage deltagere?");
                            int max = int.Parse(_input.Read());
                            @event = new Event(dateTime, title, description, _currentUser, max);
                        }
                        else
                        {
                            @event = new Event(dateTime, title, description, _currentUser);
                        }
                        _eventRepository.Add(@event);
                        break;
                    case "2":
                        _output.Write("Hvilken Begivenhed vil du slette?");
                        @event = DisplayAndSelectFromList(_eventRepository.GetAll());
                        if (IsPermittedUser(@event.Organiser))
                        {
                            _eventRepository.Remove(@event.Id);
                            _output.Write("Begivenheden er blevet slettet.");
                        }
                        else
                        {
                            _output.Write("For at slette denne begivenhed skal du være admin eller organisator.");
                        }
                        break;
                    case "3":
                        _output.Write("Hvilken begivenhed vil du se?");
                        @event = DisplayAndSelectFromList(_eventRepository.GetAll());
                        _output.Write($"{@event.ToString()}\n{@event.Description}");
                        break;
                    case "4":
                        _output.Write(_eventRepository.ToString());
                        break;
                    case "5":
                        _output.Write("Hvilken Dato vil du se? (Brug formatet \"dd-mm-yyyy\")");
                        DateOnly date = DateOnly.Parse(_input.Read());
                        _output.DisplayList(_eventRepository.GetAllOnDate(date));
                        break;
                    case "6":
                        _output.Write("Hvilken Medlems posts vil du gerne finde?");
                        Member member = DisplayAndSelectFromList(_memberRepository.GetAll());
                        _output.DisplayList(_eventRepository.GetAllByMember(member));
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }
        private void OpenSubmenuMemberRepository()
        {
            string answer;
            string question = "";
            question += $"Hvad ønsker du at gøre i dette katalog?";
            question += $"\n\t1. Tilføj Medlem";
            question += $"\n\t2. Slet Medlem";
            question += $"\n\t3. Vis specifik Medlem";
            question += $"\n\t4. Vis alle Medlemmer";
            question += $"\n\t5. Vis alle trænere";
            question += $"\n\t6. Vis alle børn";
            question += $"\n\t7. Vis alle voksne";
            question += $"\n\t8. Vis alle seniorer";
            question += $"\n\t9. Vis alle bestyrelsesmedlemmer";
            question += $"\n\tq. TILBAGE";
            while (true)
            {
                _output.Write(question);
                answer = _input.Read();
                Member member;
                switch (answer)
                {
                    case "1"://Tilføj Medlem
                        member = CreateNewMember();
                        _memberRepository.Add(member);
                        break;
                    case "2"://Slet Medlem
                        _output.Write("Hvilket Medlem vil du slette?");
                        member = DisplayAndSelectFromList(_memberRepository.GetAll());
                        if (IsPermittedUser(member))
                        {
                            _memberRepository.Remove(member.Id);
                            _output.Write("Medlemmet er blevet slettet.");
                        }
                        else
                        {
                            _output.Write("For at slette dette medlem skal du være admin eller medlemmet.");
                        }
                        break;
                    case "3"://Vis specifikt Medlem
                        _output.Write("Hvilken Blog vil du se?");
                        member = DisplayAndSelectFromList(_memberRepository.GetAll());
                        _output.Write($"{member}\nHar nøgle: {member.HasKey} | Er træner: {member.IsActiveTrainer} | Er admin: {member.IsAdmin} | Alder: {member.Age}");
                        if (_currentUser == member && GetYesNo("Ønsker du at købe en nøgle?"))
                        {
                            member.BuyKey();
                        }
                        break;
                    case "4"://Vis Medlemmer
                        _output.Write(_memberRepository.ToString());
                        break;
                    case "5"://Vis trænere
                        _output.DisplayList(_memberRepository.GetAllTrainers());
                        break;
                    case "6"://Vis børn
                        _output.DisplayList(_memberRepository.GetAllMinors());
                        break;
                    case "7"://Vis voksne
                        _output.DisplayList(_memberRepository.GetAllAdults());
                        break;
                    case "8"://Vis seniorer
                        _output.DisplayList(_memberRepository.GetAllSeniors());
                        break;
                    case "9"://Vis bestyrelsesmedlemmer
                        _output.DisplayList(_memberRepository.GetAllAdmins());
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }
        private Member CreateNewMember()
        {
            Member newMember;
            _output.Write("Hvad er dit fornavn?");
            string fistName = _input.Read();
            _output.Write("Hvad er dit efternavn?");
            string lastName = _input.Read();
            _output.Write("Hvad er din email?");
            string email = _input.Read();
            _output.Write("Hvad er din fødselsdag? (Brug formatet \"dd-mm-yyyy\")");
            DateOnly birthday = DateOnly.Parse(_input.Read());
            _output.Write("Hvad er dit telefonnummer?");
            string phone = _input.Read();
            return new Member(fistName, lastName, email, birthday, phone);
        }
        private void OpenSubmenuTeamRepository()
        {
            string answer;
            string question = "";
            question += $"Hvad ønsker du at gøre i dette katalog?";
            question += $"\n\t1. Tilføj hold";
            question += $"\n\t2. Slet hold";
            question += $"\n\t3. Vis specifik hold";
            question += $"\n\t4. Vis alle hold";
            question += $"\n\t5. Vis hold fra træner";
            question += $"\n\tq. TILBAGE";
            while (true)
            {
                _output.Write(question);
                answer = _input.Read();
                Team team;
                Member trainer;
                switch (answer)
                {
                    case "1":
                        _output.Write("Hvem skal være træner?");
                        trainer = DisplayAndSelectFromList(_memberRepository.GetAllTrainers());
                        _output.Write("Hvor mange må være på teamet?");
                        int max = int.Parse(_input.Read());
                        team = new Team(trainer, max);
                        _teamRepository.Add(team);
                        break;
                    case "2":
                        _output.Write("Hvilket Team vil du slette?");
                        team = DisplayAndSelectFromList(_teamRepository.GetAll());
                        if (_currentUser.IsAdmin)
                        {
                            _teamRepository.Remove(team.Id);
                            _output.Write("Teamet er blevet slettet.");
                        }
                        else
                        {
                            _output.Write("For at slette et team skal du være admin.");
                        }
                        break;
                    case "3":
                        _output.Write("Hvilket Team vil du se?");
                        team = DisplayAndSelectFromList(_teamRepository.GetAll());
                        _output.Write(team.ToString());
                        break;
                    case "4":
                        _output.Write(_teamRepository.ToString());
                        break;
                    case "5":
                        _output.Write("Hvilken Træner?");
                        trainer = DisplayAndSelectFromList(_memberRepository.GetAllTrainers());
                        _output.DisplayList(_teamRepository.GetAllByTrainer(trainer));
                        break;
                }
                if (answer.ToLower() == "q") break;
            }
        }

    }
}
