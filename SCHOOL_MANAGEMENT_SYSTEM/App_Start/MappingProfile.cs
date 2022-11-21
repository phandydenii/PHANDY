using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            //Domain to Dto

            //-------ROSELANY APARTMENT-----------//
            Mapper.CreateMap<RoomType, RoomTypeDto>();
            Mapper.CreateMap<Floor, FloorDto>();
            Mapper.CreateMap<Building, BuildingDto>();
            Mapper.CreateMap<Room, RoomDto>();
            Mapper.CreateMap<Item, ItemDto>();
            Mapper.CreateMap<Staff, StaffDto>();
            Mapper.CreateMap<RoomDetail, RoomDetailDto>();
            Mapper.CreateMap<Guest, GuestDto>();
            Mapper.CreateMap<CheckIn, CheckInDto>();
            Mapper.CreateMap<Booking, BookingDto>();
            Mapper.CreateMap<WaterUsage, WaterUsageDto>();
            Mapper.CreateMap<PowerUsage, PowerUsageDto>();
            Mapper.CreateMap<WaterPowerPrice, WaterPowerPriceDto>();



            //-------ROSELANY APARTMENT-----------//

            Mapper.CreateMap<HowtoUse, HowtoUseDto>();
            Mapper.CreateMap<Feedback, FeedbackDto>();
            Mapper.CreateMap<BankAccount, BankAccountDto>();
            Mapper.CreateMap<Pricing, PricingDto>();
            Mapper.CreateMap<Condition, ConditionDto>();
            Mapper.CreateMap<PayBy, PayByDto>();
            Mapper.CreateMap<PaymentMethod, PaymentMethodDto>();
            Mapper.CreateMap<Branch, BranchDto>();
            Mapper.CreateMap<Department, DepartmentDto>();
            Mapper.CreateMap<Employees, EmployeesDto>();
            Mapper.CreateMap<Salary, SalaryDto>();
            Mapper.CreateMap<Parent, ParentDto>();
            Mapper.CreateMap<Education, EducationDto>();
            Mapper.CreateMap<Experience, ExperienceDto>();
            Mapper.CreateMap<shifts, shiftDto>();
            Mapper.CreateMap<grade, gradeDto>();
            Mapper.CreateMap<student, studentDto>();
            Mapper.CreateMap<emergency, emergencyDto>();
            Mapper.CreateMap<Parrentstudent, ParrentstudentDto>();
            Mapper.CreateMap<appropriate, appropriateDto>();
            Mapper.CreateMap<registerstudent, registerstudentDto>();
            Mapper.CreateMap<studylanguage, studylanguageDto>();
            Mapper.CreateMap<studyperiod, studyperiodDto>();
            Mapper.CreateMap<course, courseDto>();
            Mapper.CreateMap<payment, paymentDto>();
            Mapper.CreateMap<paymentdetail, paymentdetailDto>();
            Mapper.CreateMap<ExchangeRate, ExchangeRateDto>();
            //New
            Mapper.CreateMap<Position, PositionDto>();
            Mapper.CreateMap<Showroom, ShowroomDto>();
            Mapper.CreateMap<Employee, EmployeeDto>();
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<Location, LocationDto>();
            Mapper.CreateMap<Invoice, InvoiceDto>();
            Mapper.CreateMap<InvoiceDetail, InvoiceDetailDto>();
            Mapper.CreateMap<Bonus, BonusDto>();
            Mapper.CreateMap<ExpenseType, ExpenseTypeDto>();
            Mapper.CreateMap<OtherExpense, OtherExpenseDto>();
            Mapper.CreateMap<Transfer, TransferDto>();
            Mapper.CreateMap<invoice_move, invoice_moveDto>();
            Mapper.CreateMap<Collectmoney, CollectmoneyDto>();
            Mapper.CreateMap<Comment, CommentDto>();

            //Dto to Domain

            //-------ROSELANY APARTMENT-----------//
            Mapper.CreateMap<RoomTypeDto,RoomType>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<FloorDto,Floor>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<BuildingDto,Building>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<RoomDto,Room>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ItemDto,Item>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<StaffDto,Staff>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<RoomDetailDto,RoomDetail>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<GuestDto,Guest>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<CheckInDto,CheckIn>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<BookingDto,Booking>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<WaterUsageDto,WaterUsage>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PowerUsageDto,PowerUsage>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<WaterPowerPriceDto, WaterPowerPrice>().ForMember(c => c.id, opt => opt.Ignore());

            //-------ROSELANY APARTMENT-----------//


            Mapper.CreateMap<HowtoUseDto, HowtoUse>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<FeedbackDto, Feedback>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<BankAccountDto, BankAccount>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PricingDto, Pricing>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ConditionDto, Condition>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PayByDto, PayBy>().ForMember(c => c.id, opt => opt.Ignore());

            Mapper.CreateMap<PaymentMethodDto, PaymentMethod>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<BranchDto,Branch>().ForMember(c=>c.Id,opt=>opt.Ignore());
            Mapper.CreateMap<DepartmentDto, Department>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<EmployeesDto, Employees>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<SalaryDto, Salary>().ForMember(c => c.salaryId, opt => opt.Ignore());
            Mapper.CreateMap<ParentDto, Parent>().ForMember(c => c.parrentId, opt => opt.Ignore());
            Mapper.CreateMap<EducationDto, Education>().ForMember(c => c.educationId, opt => opt.Ignore());
            Mapper.CreateMap<ExperienceDto, Experience>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<shiftDto, shifts>().ForMember(c => c.shiftid, opt => opt.Ignore());
            Mapper.CreateMap<gradeDto, grade>().ForMember(c => c.gradeid, opt => opt.Ignore());
            Mapper.CreateMap<studentDto, student>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<emergencyDto, emergency>().ForMember(c => c.emerid, opt => opt.Ignore());
            Mapper.CreateMap<ParrentstudentDto, Parrentstudent>().ForMember(c => c.parrentId, opt => opt.Ignore());
            Mapper.CreateMap<appropriateDto, appropriate>().ForMember(c => c.appid, opt => opt.Ignore());
            Mapper.CreateMap<registerstudentDto, registerstudent>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<studylanguageDto, studylanguage>().ForMember(c => c.studylanguageid, opt => opt.Ignore());
            Mapper.CreateMap<studyperiodDto, studyperiod>().ForMember(c => c.studyperiodid, opt => opt.Ignore());
            Mapper.CreateMap<courseDto, course>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<paymentDto, payment>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<paymentdetailDto, paymentdetail>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ExchangeRateDto, ExchangeRate>().ForMember(c => c.id, opt => opt.Ignore());
           
            //New
            Mapper.CreateMap<PositionDto, Position>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ShowroomDto, Showroom>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<EmployeeDto, Employee>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<CategoryDto, Category>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ProductDto, Product>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<LocationDto, Location>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<InvoiceDto, Invoice>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<InvoiceDetailDto, InvoiceDetail>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<BonusDto, Bonus>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ExpenseTypeDto, ExpenseType>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<OtherExpenseDto, OtherExpense>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<TransferDto, Transfer>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<invoice_moveDto, invoice_move>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<CollectmoneyDto, Collectmoney>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<CommentDto, Comment>().ForMember(c => c.id, opt => opt.Ignore());

        }
    }
}