import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { SystemEntity, IExamCategory, ExamTypeDisplayName } from '../../../models/administraion.models';
import { ApiGateway } from '../../../api/api.gateway';

@Component({})
export default class ManageComponent extends Vue {
    apiGateway = new ApiGateway();

    SystemEntityEnum = SystemEntity;
    currentSystemEntity: SystemEntity = SystemEntity.Candidate;
    items: any = [];
    pagination = {
        rowsPerPage: 5,
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
                        value: 'id'
                    },
                    {
                        text: 'Nazwa',
                        value: 'name'
                    },
                    {
                        text: 'Data',
                        value: 'startDateTime'
                    },
                    {
                        text: 'Kategoria',
                        value: 'examCategory.name'
                    }
                ];
            case SystemEntity.ExamCategory:
                return [
                    {
                        text: 'ID Kategorii',
                        value: 'id'
                    },
                    {
                        text: 'Nazwa',
                        value: 'name'
                    },
                    {
                        text: 'Typ Egzaminu',
                        value: 'examTypeName'
                    }
                ];
            case SystemEntity.Teacher:
                return [
                    {
                        text: 'ID nauczyciela',
                        value: 'id'
                    },
                    {
                        text: 'Imię',
                        value: 'name'
                    },
                    {
                        text: 'Nazwisko',
                        value: 'surname'
                    }
                ];
        }
    }

    @Watch('pagination')
    onPropertyChanged(value: any, oldValue: any) {
        //Listing Candidates supports pagination since its the only entity that we estimate that there will be above a hundred
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
        this.currentSystemEntity = ent;
        this.$router.push(`/adminPanel/manage/${ent}`);
        this.fetchItems(this.currentSystemEntity, this.pagination);
    }

    handleSelect(entity: any) {
        this.$router.push(`/adminPanel/details/${this.currentSystemEntity}/${entity.id}`);
    }

    fetchItems(ent: SystemEntity, pagingParams: any) {
        switch (this.currentSystemEntity) {
            case SystemEntity.ExamCategory:
                this.apiGateway.listExamCategories().then(resp => {
                    console.log(resp);
                    this.items = resp.map((category: IExamCategory) => {
                        return {
                            id: category.id,
                            name: category.name,
                            examTypeName: ExamTypeDisplayName(category.examType)
                        };
                    });
                    this.pagination.rowsPerPage = resp.length;
                    this.pagination.page = 1;
                })
            default:
        }
    }
}
