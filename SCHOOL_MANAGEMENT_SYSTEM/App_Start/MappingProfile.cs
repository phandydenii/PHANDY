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
            Mapper.CreateMap<ElectricUsage, ElectricUsageDto>();
            Mapper.CreateMap<WEPrice, WEPriceDto>();
            Mapper.CreateMap<CheckOut, CheckOutDto>();
            Mapper.CreateMap<CheckOutDeatil, CheckOutDetailDto>();
            Mapper.CreateMap<Salary, SalaryDto>();
            Mapper.CreateMap<PaySlip, PaySlipDto>();
            Mapper.CreateMap<PayDemage, PayDemageDto>();
            Mapper.CreateMap<WaterElectricUsage, WaterElectricUsageDto>();

            //-------ROSELANY APARTMENT-----------//

            Mapper.CreateMap<PayBy, PayByDto>();
            Mapper.CreateMap<PaymentMethod, PaymentMethodDto>();
            Mapper.CreateMap<Branch, BranchDto>();
            Mapper.CreateMap<ExchangeRate, ExchangeRateDto>();
            //New
            Mapper.CreateMap<Position, PositionDto>();
            Mapper.CreateMap<Invoice, InvoiceDto>();
            Mapper.CreateMap<InvoiceDetail, InvoiceDetailDto>();
            Mapper.CreateMap<ExpenseType, ExpenseTypeDto>();
            Mapper.CreateMap<OtherExpense, OtherExpenseDto>();
            Mapper.CreateMap<Employees, EmployeesDto>();
            Mapper.CreateMap<Department, DepartmentDto>();

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
            Mapper.CreateMap<ElectricUsageDto,ElectricUsage>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<WEPriceDto, WEPrice>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<CheckOutDto, CheckOut>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<CheckOutDetailDto, CheckOutDeatil>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<SalaryDto, Salary>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PaySlipDto, PaySlip>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PayDemageDto, PayDemage>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<WaterElectricUsageDto, WaterElectricUsage>().ForMember(c => c.id, opt => opt.Ignore());

            //-------ROSELANY APARTMENT-----------//


            Mapper.CreateMap<PayByDto, PayBy>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PaymentMethodDto, PaymentMethod>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<BranchDto, Branch>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<ExchangeRateDto, ExchangeRate>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<PositionDto, Position>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<InvoiceDto, Invoice>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<InvoiceDetailDto, InvoiceDetail>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<ExpenseTypeDto, ExpenseType>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<OtherExpenseDto, OtherExpense>().ForMember(c => c.id, opt => opt.Ignore());
            Mapper.CreateMap<EmployeesDto, Employees>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<DepartmentDto, Department>().ForMember(c => c.Id, opt => opt.Ignore());


        }
    }
}