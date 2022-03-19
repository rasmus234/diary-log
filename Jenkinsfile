pipeline{
    agent any
    stages{
        stage("Build API") {
            when {
                anyOf {
                    changeset "DiaryLog/**"
                }
            }
            steps{
                dir("DiaryLog") {
                    sh "dotnet build --configuration Release"
                    sh "sudo docker-compose build api"
                }
            }
        }
        stage("Build frontend") {
            when {
                changeset "diary-log-angular/**"
            }
            steps {
                dir("diary-log-angular") {
					sh "sudo rmdir dist -f"
					sh "sudo ng build"
                    sh "sudo docker-compose build web"
                }
            }
        }
        stage("Unit Tests") {
            steps {
                echo "Running tests"
                dir("DiaryLog") {
                    sh "dotnet test"
                }
            }
        }
        stage("Clean containers") {
            steps {
                script {
                    try {
                        sh "sudo docker-compose down"
                    }
                    finally { }
                }
            }
        }
        stage("Deploy") {
            steps {
                sh "sudo docker-compose up -d"
            }
        }
        stage("Discord Notification") {
            steps {
                discordSend description: 'Build completed', enableArtifactsList: true, footer: '', image: '', link: '', result: 'SUCCESS', scmWebUrl: 'https://github.com/rasmus234/diary-log', showChangeset: true, thumbnail: '', title: 'Diary Log', webhookURL: 'https://discord.com/api/webhooks/951847255734911016/Vfz5r9qnougI2rLhXfKyleBoToWPTTepmQmB_plN_cf4Fm5VZIYkuoJ4V33haopGg_gb'
            }
        }
    }
}