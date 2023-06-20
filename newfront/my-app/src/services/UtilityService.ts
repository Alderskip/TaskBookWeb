export default class UtilityService {

    static  getMonthName(monthNumber:number):string {
        switch (monthNumber)
        {
            case 1:{
                return "Янв."
            }
            case 2:{
                return "Фер."
            }
            case 3:{
                return "Мар."
            }
            case 4:{
                return "Апр."
            }
            case 5:{
                return "Мая"
            }
            case 6:{
                return "Июня"
            }
            case 7:{
                return "Июля"
            }
            case 8:{
                return "Авг."
            }
            case 9:{
                return "Сен."
            }
            case 10:{
                return "Окт."
            }
            case 11:{
                return "Ноя."
            }
            case 12:{
                return "Дек."
            }
            default:
                return"Неизвестно"
        }
    }


    }