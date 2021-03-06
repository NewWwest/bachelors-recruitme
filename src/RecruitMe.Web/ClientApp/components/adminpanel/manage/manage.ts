import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { SystemEntity, IExamCategory, IExam } from '../../../models/administraion.models';
import { ApiGateway } from '../../../api/api.gateway';
import { toLocalTime } from '../../../helpers/datetime.helper';
import { ExamTypeDisplayName } from '../../../helpers/examType.helper';
import { MessageBusService } from '../../../services/messageBus.service';
import { getErrorMessage } from '../../../helpers/error.helper';

@Component({})
export default class ManageComponent extends Vue {
    apiGateway = new ApiGateway();

    examCategories: IExamCategory[] = [];
    SystemEntityEnum = SystemEntity;
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;
    rowsTotal = 0;
    items: any = [];
    pagination: any = {
        itemsPerPage: 5,
        page: 1
    }

    headers() {
        switch (this.currentSystemEntity) {
            case SystemEntity.Candidate:
                return [
                    {
                        text: 'ID kandydata',
                        value: 'candidateId'
                    },
                    {
                        text: 'Imię',
                        value: 'name'
                    },
                    {
                        text: 'Nazwisko',
                        value: 'surname'
                    },
                    {
                        text: 'Email',
                        value: 'email'
                    }
                ];
            case SystemEntity.Exam:
                return [
                    {
                        text: 'ID egzaminu',
                        value: 'id',
                        sortable: false,
                    },
                    {
                        text: 'Czas rozpoczęcia',
                        value: 'startDateTime',
                        sortable: false,
                    },
                    {
                        text: 'Czas per kandydat [min]',
                        value: 'durationInMinutes',
                        sortable: false,
                    },
                    {
                        text: 'Liczba miejsc',
                        value: 'seatCount',
                        sortable: false,
                    },
                    {
                        text: 'Kategoria',
                        value: 'examCategory',
                        sortable: false,
                    }
                ];
            case SystemEntity.ExamCategory:
                return [
                    {
                        text: 'ID Kategorii',
                        value: 'id',
                        sortable: false,
                    },
                    {
                        text: 'Nazwa',
                        value: 'name',
                        sortable: false,
                    },
                    {
                        text: 'Typ Egzaminu',
                        value: 'examTypeName',
                        sortable: false,
                    }
                ];
            case SystemEntity.Teacher:
                return [
                    {
                        text: 'ID nauczyciela',
                        value: 'id',
                        sortable: false,
                    },
                    {
                        text: 'Imię',
                        value: 'name',
                        sortable: false,
                    },
                    {
                        text: 'Nazwisko',
                        value: 'surname',
                        sortable: false,
                    },
                    {
                        text: 'Adres Email',
                        value: 'email',
                        sortable: false,
                    }
                ];
        }
    }

    @Watch('pagination')
    onPropertyChanged(value: any, oldValue: any) {
        //Paging implemented only for users
        if (this.currentSystemEntity == SystemEntity.Candidate)
            this.fetchItems(this.currentSystemEntity, value)
    }

    mounted() {
        let type = this.$route.params.entityType;
        if (type == SystemEntity.Candidate ||
            type == SystemEntity.Exam ||
            type == SystemEntity.ExamCategory ||
            type == SystemEntity.Teacher) {
            this.currentSystemEntity = type;
            this.fetchItems(type, this.pagination);
        }
        else {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }
    }

    changeEntity(ent: SystemEntity) {
        if (ent == this.currentSystemEntity)
            return;

        this.currentSystemEntity = ent;
        this.$router.push(`/adminPanel/manage/${ent}`);
        this.items = [];
        this.fetchItems(this.currentSystemEntity, this.pagination);
    }

    handleSelect(entity: any) {
        this.$router.push(`/adminPanel/details/${this.currentSystemEntity}/${entity.id}`);
    }

    fetchItems(ent: SystemEntity, pagingParams: any) {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.listExamCategories().then(resp => {
                    this.examCategories = resp;
                    this.items = resp.map((category: IExamCategory) => {
                        return {
                            id: category.id,
                            name: category.name,
                            examTypeName: ExamTypeDisplayName(category.examType)
                        };
                    });

                    this.rowsTotal = resp.length;
                    this.pagination.page = 1;
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            case SystemEntity.Teacher:
                this.apiGateway.listTeachers().then(resp => {
                    this.items = resp;
                    this.rowsTotal = resp.length;
                    this.pagination.page = 1;
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            case SystemEntity.Exam:
                this.apiGateway.listExams().then(exams => {
                    if (!this.examCategories || this.examCategories.length == 0) {
                        this.apiGateway.listExamCategories().then(categories => {
                            this.examCategories = categories;
                            this.mapExams(exams);
                        }, err => MessageBusService.emitError(getErrorMessage(err)));
                    } else {
                        this.mapExams(exams);
                    }
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            case SystemEntity.Candidate:
                let paging: any = {
                    page: this.pagination.page,
                    pageSize: this.pagination.itemsPerPage,
                    sortBy: this.pagination.sortBy[0],
                    sortDesc: this.pagination.sortDesc[0]
                }
                this.apiGateway.listCandidates(paging).then(x => {
                    this.items = x.data;
                    this.rowsTotal = x.totalCount;
                }, err => MessageBusService.emitError(getErrorMessage(err)));
                break;
            default:
        }
    }

    mapExams(exams: IExam[]) {
        this.items = exams.map((e: IExam) => {
            return {
                id: e.id,
                seatCount: e.seatCount,
                startDateTime: toLocalTime(e.startDateTime).toLocaleDateString("pl") + " " + toLocalTime(e.startDateTime).toLocaleTimeString("pl"),
                durationInMinutes: e.durationInMinutes,
                examCategory: this.examCategories.find(ec => ec.id == e.examCategoryId)?.name
            };
        });
        this.rowsTotal = exams.length;
        this.pagination.page = 1;
    }
}
